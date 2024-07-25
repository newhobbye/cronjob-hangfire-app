using hangfire_jobs_service.Interfaces;
using hangfire_jobs_service.Models.Response.ViaCep;
using RestSharp;
using System.Net;
using System.Text.Json;

namespace hangfire_jobs_service.Services
{
    public class RequestsService: IRequestsService
    {

        public async Task<ViaCepResponse> GetViaCepDataAsync(string cep)
        {
            //arquivo de config
            var client = new RestClient("");

            var request = new RestRequest("", Method.Get);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return JsonSerializer.Deserialize<ViaCepResponse>(response.Content!) ?? null;
            }

            return null;
        }
    }
}
