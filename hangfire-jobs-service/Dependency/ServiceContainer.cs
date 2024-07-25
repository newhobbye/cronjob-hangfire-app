using hangfire_jobs_database.Dependency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hangfire_jobs_service.Dependency
{
    public static class ServiceContainer
    {
        public static void RegisterServiceContainer(IServiceCollection services)
        {
            DatabaseContainer.RegisterDatabaseContainer(services);
        }
    }
}
