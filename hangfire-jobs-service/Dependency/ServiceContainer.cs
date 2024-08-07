using Hangfire.Dashboard;
using Hangfire;
using hangfire_jobs_database.Dependency;
using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using hangfire_jobs_service.Filters;
using Hangfire.MemoryStorage;

namespace hangfire_jobs_service.Dependency
{
    public static class ServiceContainer
    {
        public static void RegisterServiceContainer(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseContainer.RegisterDatabaseContainer(services, configuration);

            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHangfire(config =>
                config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage());

            JobStorage.Current = new MemoryStorage();

            services.AddHangfireServer();
        }

        public static void RegisterServiceAppContainer(WebApplication app, IConfiguration configuration)
        {
            //precisa ser chamado antes da definição dos jobs
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },
                IsReadOnlyFunc = (DashboardContext context) => true
            });
        }

        public static void HangfireJobsConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            var userService = serviceProvider.GetRequiredService<IUserService>();
            //var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

            RecurringJob.AddOrUpdate("Verificação de endereços dos clientes",
                () => userService.VerifyAddressesOfUsersAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronTwoInTwoMinutes"));

            RecurringJob.AddOrUpdate("Resetar os endereços para operação do hangfire",
                () => userService.ResetUsersForHangfireOperationAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronFiveInFiveMinutes"));

            serviceProvider.Dispose();
        }
    }
}
