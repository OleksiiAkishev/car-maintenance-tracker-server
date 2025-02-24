using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Services.UserService
{
    public interface IUserService
    {
        Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto user);
        Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto user);
        Task<GetUserResponse> GetUserById(Guid userId);
        Task<GetUserResponse> GetUserByUsername(string username);
        Task<UpdateUserResponseDto> UpdateUser(Guid userId, UpdateUserRequestDto user);
        Task<bool> DeleteUser(Guid userId);
    }
}
