using hangfire_jobs_service.Interfaces;
using hangfire_jobs_tests.Dependency;

namespace hangfire_jobs_tests.Services
{
    public class RequestServiceTests
    {
        private readonly IRequestsService _requestsService;

        public RequestServiceTests()
        {
            _requestsService = GetServiceInjection.GetService<IRequestsService>();
        }

        [Fact(DisplayName = "Get na api do viaCep")]//, Skip = "Desativar xTests azure"
        public async Task GetViaCepDataAsync()
        {
            string cep = "06448150";

            var result = await _requestsService.GetViaCepDataAsync(cep);

            Assert.NotNull(result);
        }
    }
}
