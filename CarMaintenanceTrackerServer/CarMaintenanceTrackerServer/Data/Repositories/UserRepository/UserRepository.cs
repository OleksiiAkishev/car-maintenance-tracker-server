using CarMaintenanceTrackerServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServer.Data.Repositories.UserRepository
{
    public class UserRepository(CarMaintenanceTrackerDbContext dbContext) : IUserRepository
    {
        private readonly CarMaintenanceTrackerDbContext dbContext = dbContext;

        public async Task<User> RegisterUser(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginUser(int userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await dbContext.Users.FindAsync(userId);
            if (user == null) 
            {
                return false;
            }
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
