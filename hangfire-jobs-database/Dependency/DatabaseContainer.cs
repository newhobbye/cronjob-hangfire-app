using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace hangfire_jobs_database.Dependency
{
    public static class DatabaseContainer
    {
        public static void RegisterDatabaseContainer(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=localdatabase.db"));
        }
    }
}
