using CarMaintenanceTrackerServer.DTOs.User.Response;
using UserEntity = CarMaintenanceTrackerServer.Entities.User;

namespace CarMaintenanceTrackerServer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserEntity> RegisterUser(UserEntity user);
        Task<UserEntity?> LoginUser(int userId);
        Task<UserEntity?> GetUserById(int userId);
        Task<UserEntity?> GetUserByUsername(string username);
        Task<UserEntity> UpdateUser(UserEntity user);
        Task<bool> DeleteUser(int userId); 
    }
}
