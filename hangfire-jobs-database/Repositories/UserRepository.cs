using hangfire_jobs_database.Context;
using hangfire_jobs_database.Interfaces;
using hangfire_jobs_database.Models;
using Microsoft.EntityFrameworkCore;

namespace hangfire_jobs_database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LocalDbContext _context;

        public UserRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var result = await _context.Users
                .Include(x => x.Addresses)
                .ToListAsync();

            return result;
        }

        public async Task<bool> UpdateByIdAsync(string id, User user)
        {
            var userUpdate = await GetByIdAsync(id);

            if (userUpdate != null) 
            {
                userUpdate.Email = user.Email ?? userUpdate.Email;
                userUpdate.Name = user.Name ?? userUpdate.Name;
                userUpdate.Phone = user.Phone ?? userUpdate.Phone;

                int result = await _context.SaveChangesAsync();

                return result > 0 ? true : false;
            }

            return false;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var result = await _context.Users
                .Include(x => x.Addresses)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Address> GetAddressByIdAsync(string id)
        {
            var result = await _context.Addresses
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateAddressByIdAsync(string id, Address updateAddress)
        {
            var addressUpdate = await GetAddressByIdAsync(id);

            if (addressUpdate != null)
            {
                addressUpdate.Street = updateAddress.Street ?? addressUpdate.Street;
                addressUpdate.State = updateAddress.State ?? addressUpdate.State;
                addressUpdate.Neightborhood = updateAddress.Neightborhood ?? addressUpdate.Neightborhood;
                addressUpdate.Country = updateAddress.Country ?? addressUpdate.Country;

                int result = await _context.SaveChangesAsync();

                return result > 0 ? true : false;
            }

            return false;
        }
    }
}
