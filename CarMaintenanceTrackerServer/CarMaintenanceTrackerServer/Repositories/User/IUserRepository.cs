using CarMaintenanceTrackerServer.DTOs.User.Response;
using UserEntity = CarMaintenanceTrackerServer.Entities.User;

namespace CarMaintenanceTrackerServer.Repositories.User
{
    public interface IUserRepository
    {
        Task<UserRegisterResponseDto> RegisterUser(UserEntity user);
        Task<UserLoginResponseDto> LoginUser(int userId);
    }
}
