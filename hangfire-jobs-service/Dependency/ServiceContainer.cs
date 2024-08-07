using Hangfire.Dashboard;
using Hangfire;
using hangfire_jobs_database.Dependency;
using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using hangfire_jobs_service.Filters;
using Hangfire.SQLite;

namespace hangfire_jobs_service.Dependency
{
    public static class ServiceContainer
    {
        public static void RegisterServiceContainer(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseContainer.RegisterDatabaseContainer(services, configuration);

            services.AddScoped<IRequestsService, RequestsService>();
            services.AddScoped<IUserService, UserService>();

            string connectionString = configuration.GetConnectionString("SqLite");

            services.AddHangfire(config =>
            config.UseSQLiteStorage($@"Data Source=C:\\Users\\Note_Samsung01\\source\\repos\\hangfire-jobs\\hangfire-jobs-database\\DatabaseFile\\localDb.db"));

            services.AddHangfireServer();

            HangfireJobsConfiguration(services, configuration);
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
            var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();

            recurringJobManager.AddOrUpdate("Criar kyc para estabelecimento homologado",
                () => userService.VerifyAddressesOfUsersAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronHourByHour"));

            recurringJobManager.AddOrUpdate("Criar kyc para estabelecimento onboarding sem kyc",
                () => userService.ResetUsersForHangfireOperationAsync(),
                configuration.GetValue<string>("HangfireOperationSettings:CronHourByHour"));

            serviceProvider.Dispose();
        }
    }
}
