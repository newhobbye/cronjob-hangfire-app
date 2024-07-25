using hangfire_jobs_database.Dependency;
using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Services;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
