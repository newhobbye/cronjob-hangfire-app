using hangfire_jobs_database.Interfaces;
using hangfire_jobs_database.Models;
using hangfire_jobs_service.Interfaces;
using System.Collections.Generic;

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
                        addressOfId.Street = resultViaCep.Logradouro;
                        addressOfId.Neightborhood = resultViaCep.Bairro;
                        addressOfId.State = resultViaCep.Uf;
                        addressOfId.Country = "Brasil";

                        _ = _repo.UpdateAddressByIdAsync(id, addressOfId);
                    }
                }
                else continue;
            }
        }
        #endregion
    }
}
