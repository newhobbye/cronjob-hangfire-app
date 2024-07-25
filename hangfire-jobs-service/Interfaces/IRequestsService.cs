using hangfire_jobs_service.Models.Response.ViaCep;

namespace hangfire_jobs_service.Interfaces
{
    public interface IRequestsService
    {
        Task<ViaCepResponse> GetViaCepDataAsync(string cep);
    }
}
