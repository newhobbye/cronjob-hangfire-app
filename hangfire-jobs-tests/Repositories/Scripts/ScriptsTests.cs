using hangfire_jobs_database.Context;
using hangfire_jobs_database.Models;
using hangfire_jobs_tests.Dependency;

namespace hangfire_jobs_tests.Repositories.Scripts
{
    public class ScriptsTests
    {
        private readonly LocalDbContext _context;

        public ScriptsTests()
        {
            _context = GetServiceInjection.GetService<LocalDbContext>();
        }

        [Fact(DisplayName = "Script para criar usuarios iniciais", Skip = "Desativado")] //
        public async Task CreateUsersAndAddressesAsync()
        {
            var newUserOne = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "teste-um@teste.com",
                Name = "Teste Um",
                Phone = "11987278321",
                Addresses = new List<Address> 
                { 
                    new Address 
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cep = "06448150",
                        Number = "56",
                        Street = "",
                        State = "",
                        Neightborhood = "",
                        Country = ""
                    },
                    new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cep = "01001000",
                        Number = "48",
                        Street = "",
                        State = "",
                        Neightborhood = "",
                        Country = ""
                    }
                }
            };

            foreach (var address in newUserOne.Addresses)
            {
                address.UserId = newUserOne.Id;
            }

            _context.Users.Add(newUserOne);
            await _context.SaveChangesAsync();

            var newUserTwo = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "teste-dois@teste.com",
                Name = "Teste Dois",
                Phone = "11987278321",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cep = "08000000",
                        Number = "56",
                        Street = "",
                        State = "",
                        Neightborhood = "",
                        Country = ""
                    },
                    new Address
                    {
                        Id = Guid.NewGuid().ToString(),
                        Cep = "08100000",
                        Number = "48",
                        Street = "",
                        State = "",
                        Neightborhood = "",
                        Country = ""
                    }
                }
            };

            foreach (var address in newUserTwo.Addresses)
            {
                address.UserId = newUserTwo.Id;
            }

            _context.Users.Add(newUserTwo);
            await _context.SaveChangesAsync();
        }
    }
}
