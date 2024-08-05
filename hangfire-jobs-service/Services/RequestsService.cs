using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Models.Response.ViaCep;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace hangfire_jobs_service.Services
{
    public class RequestsService: IRequestsService
    {
        private readonly IConfiguration _config;

        public RequestsService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ViaCepResponse> GetViaCepDataAsync(string cep)
        {
            var client = new RestClient(_config["ViaCepUrl"]);

            var request = new RestRequest($@"ws/{cep}/json", Method.Get);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return JsonSerializer.Deserialize<ViaCepResponse>(response.Content!) ?? null;
            }

            return null;
        }
    }
}
