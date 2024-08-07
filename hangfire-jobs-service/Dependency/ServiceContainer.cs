using Hangfire.Dashboard;
using Hangfire;
using hangfire_jobs_database.Dependency;
using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using hangfire_jobs_service.Filters;

namespace hangfire_jobs_service.Dependency
{
    public static class ServiceContainer
    {
        public static void RegisterServiceContainer(IServiceCollection services)
        {
            DatabaseContainer.RegisterDatabaseContainer(services);

            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void RegisterServiceAppContainer(WebApplication app, IConfiguration configuration)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },
                IsReadOnlyFunc = (DashboardContext context) => true
            });
        }

        private static void HangfireJobsConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();

            var userService = serviceProvider.GetRequiredService<IUserService>();

            RecurringJob.AddOrUpdate("Criar kyc para estabelecimento homologado",
                () => userService.VerifyAddressesOfUsersAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronHourByHour"));

            RecurringJob.AddOrUpdate("Criar kyc para estabelecimento onboarding sem kyc",
                () => userService.ResetUsersForHangfireOperationAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronHourByHour"));


            serviceProvider.Dispose();
        }
    }
}
