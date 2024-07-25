using hangfire_jobs_database.Context;
using hangfire_jobs_database.Models;

namespace hangfire_jobs_database.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<bool> UpdateByIdAsync(string id, User user);
        Task<User> GetByIdAsync(string id);
        Task<Address> GetAddressByIdAsync(string id);
        Task<bool> UpdateAddressByIdAsync(string id, Address updateAddress);
    }
}
