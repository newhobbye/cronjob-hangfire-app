using hangfire_jobs_database.Context;
using hangfire_jobs_database.Interfaces;
using hangfire_jobs_database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace hangfire_jobs_database.Dependency
{
    public static class DatabaseContainer
    {
        public static void RegisterDatabaseContainer(IServiceCollection services)
        {
            var projectRootPath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = @"C:\Users\Note_Samsung01\source\repos\hangfire-jobs\hangfire-jobs-database\DatabaseFile\localDb.db";

            services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
