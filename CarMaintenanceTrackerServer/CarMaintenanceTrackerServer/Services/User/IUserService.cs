using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Services.User
{
    public interface IUserService
    {
        Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto user);
        Task<UserLoginResponseDto> LoginUser(UserLoginRequestDto user);
    }
}
