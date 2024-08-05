using hangfire_jobs_service.Interfaces;
using hangfire_jobs_tests.Dependency;

namespace hangfire_jobs_tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userService = GetServiceInjection.GetService<IUserService>();
        }

        [Fact(DisplayName = "Verificar ceps de usuarios com endereço fora do padrão", Skip = "Build")] //
        public async Task VerifyAddressesOfUsersAsync()
        {
            try
            {
                await _userService.VerifyAddressesOfUsersAsync();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        [Fact(DisplayName = "Resetar cadastros para testar novamente o hangfire", Skip = "Build")] //
        public async Task ResetUsersForHangfireOperationAsync()
        {
            try
            {
                await _userService.ResetUsersForHangfireOperationAsync();
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}
