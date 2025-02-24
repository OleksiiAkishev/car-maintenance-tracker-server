using CarMaintenanceTrackerServer.Data.Entities;

namespace CarMaintenanceTrackerServer.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<User?> LoginUser(Guid userId);
        Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByUsername(string username);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(Guid userId); 
    }
}
