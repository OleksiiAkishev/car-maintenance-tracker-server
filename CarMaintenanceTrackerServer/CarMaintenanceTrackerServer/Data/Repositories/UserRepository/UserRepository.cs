using CarMaintenanceTrackerServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceTrackerServer.Data.Repositories.UserRepository
{
    public class UserRepository(ServerDbContext dbContext) : IUserRepository
    {
        private readonly ServerDbContext dbContext = dbContext;

        public async Task<User> RegisterUser(User user)
        {
            this.dbContext.Users.Add(user);
            await this.dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginUser(Guid userId)
        {
            return await this.dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            return await this.dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await this.dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> UpdateUser(User user)
        {
            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
            user.IsDeleted = true;
            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
