using CarMaintenanceTrackerServer.Data.Entities;

namespace CarMaintenanceTrackerServer.Data.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(User user);
        Task<User?> LoginUser(int userId);
        Task<User?> GetUserById(int userId);
        Task<User?> GetUserByUsername(string username);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(int userId); 
    }
}
