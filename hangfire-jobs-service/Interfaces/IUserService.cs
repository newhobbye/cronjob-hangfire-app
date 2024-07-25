namespace hangfire_jobs_service.Interfaces
{
    public interface IUserService
    {
        Task VerifyAddressesOfUsersAsync();
        Task ResetUsersForHangfireOperationAsync();
    }
}
