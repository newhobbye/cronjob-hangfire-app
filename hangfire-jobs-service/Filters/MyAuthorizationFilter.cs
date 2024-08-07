using Hangfire.Dashboard;

namespace hangfire_jobs_service.Filters
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            //var httpContext = context.GetHttpContext();

            //return httpContext.User.Identity?.IsAuthenticated ?? false;
            return true;
        }
    }
}
