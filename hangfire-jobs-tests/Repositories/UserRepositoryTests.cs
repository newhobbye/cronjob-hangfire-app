using hangfire_jobs_database.Interfaces;
using hangfire_jobs_tests.Dependency;

namespace hangfire_jobs_tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _userRepo;

        public UserRepositoryTests()
        {
            _userRepo = GetServiceInjection.GetService<IUserRepository>();
        }

        [Fact(DisplayName = "Pegar todos os usuarios e endereços deles")]// Skip = "Build"
        public async Task GetAllUsersAsync()
        {
            var result = await _userRepo.GetAllAsync();

            Assert.NotNull(result);
        }
    }
}
