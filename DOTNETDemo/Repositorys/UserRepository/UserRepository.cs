using DOTNETDemo.Data;
using DOTNETDemo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOTNETDemo.Repositorys.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsyncById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                // Always add a new user
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Log exception details here if needed
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                return false; // User not found
            }

            // Update fields
            existingUser.Name = user.Name;
            existingUser.CardNumber = user.CardNumber;
            existingUser.CVC = user.CVC;
            existingUser.ExpiryDate = user.ExpiryDate;

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task DeleteUserAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
