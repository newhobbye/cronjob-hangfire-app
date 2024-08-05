using hangfire_jobs_database.Interfaces;
using hangfire_jobs_database.Models;
using hangfire_jobs_service.Interfaces;

namespace hangfire_jobs_service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IRequestsService _request;

        public UserService(IUserRepository repo, IRequestsService request)
        {
            _repo = repo;
            _request = request;
        }

        public async Task VerifyAddressesOfUsersAsync()
        {
            var users = await _repo.GetAllAsync();

            foreach (var user in users)
            {
                var idsOfAddressesWithEmptyProperties = VerifyAddressesOfUser(user.Addresses.ToList());

                await SendToViaCepForGetMoreDataAsync(idsOfAddressesWithEmptyProperties);
            }
        }

        public async Task ResetUsersForHangfireOperationAsync()
        {
            var users = await _repo.GetAllAsync();

            foreach (var user in users)
            {
                if(user.Addresses.Count > 0)
                {
                    foreach (var address in user.Addresses)
                    {
                        address.Street = "";
                        address.State = "";
                        address.Neightborhood = "";
                        address.Country = "";

                        _ = _repo.UpdateAddressByIdAsync(address.Id, address);
                    }
                }
            }
        }

        #region[Metodos auxiliares]

        private List<string> VerifyAddressesOfUser(List<Address> addresses)
        {
            var idsOfAddressesWithEmptyProperties = new List<string>();

            foreach (var address in addresses)
            {
                if (string.IsNullOrEmpty(address.Street) || string.IsNullOrEmpty(address.State)
                    || string.IsNullOrEmpty(address.Neightborhood) || string.IsNullOrEmpty(address.Country))
                {
                    idsOfAddressesWithEmptyProperties.Add(address.Id);
                }
                else continue;
            }

            return idsOfAddressesWithEmptyProperties;
        }

        private async Task SendToViaCepForGetMoreDataAsync(List<string> idsAddresses)
        {
            foreach (var id in idsAddresses)
            {
                var addressOfId = await _repo.GetAddressByIdAsync(id);

                if (addressOfId is not null)
                {
                    var resultViaCep = await _request.GetViaCepDataAsync(addressOfId.Cep);

                    if (resultViaCep is not null)
                    {
                        addressOfId.Street = resultViaCep.Logradouro ?? addressOfId.Street;
                        addressOfId.Neightborhood = resultViaCep.Bairro ?? addressOfId.Neightborhood;
                        addressOfId.State = resultViaCep.Uf ?? addressOfId.State;
                        addressOfId.Country = resultViaCep.Logradouro is null ? "" : "Brasil";

                        _ = _repo.UpdateAddressByIdAsync(id, addressOfId);
                    }
                }
                else continue;
            }
        }
        #endregion
    }
}
