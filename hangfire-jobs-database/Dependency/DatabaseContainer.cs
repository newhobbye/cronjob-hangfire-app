using hangfire_jobs_database.Context;
using hangfire_jobs_database.Interfaces;
using hangfire_jobs_database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hangfire_jobs_database.Dependency
{
    public static class DatabaseContainer
    {
        public static void RegisterDatabaseContainer(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlite(config["ConnectionStrings:SqLite"]));

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
