using CarMaintenanceTrackerServer.Data;
using Microsoft.EntityFrameworkCore;
using UserEntity = CarMaintenanceTrackerServer.Entities.User;

namespace CarMaintenanceTrackerServer.Repositories.User
{
    public class UserRepository(CarMaintenanceTrackerDbContext dbContext) : IUserRepository
    {
        private readonly CarMaintenanceTrackerDbContext dbContext = dbContext;

        public async Task<UserEntity> RegisterUser(UserEntity user)
        {
            this.dbContext.Users.Add(user);
            await this.dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<UserEntity?> LoginUser(int userId)
        {
            return await this.dbContext.Users.FindAsync(userId);
        }

        public async Task<UserEntity?> GetUserById(int userId)
        {
            return await this.dbContext.Users.FindAsync(userId);
        }

        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            return await this.dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<UserEntity> UpdateUser(UserEntity user)
        {
            this.dbContext.Users.Update(user);
            await this.dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await this.dbContext.Users.FindAsync(userId);
            if (user == null) 
            {
                return false;
            }
            this.dbContext.Users.Remove(user);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}
