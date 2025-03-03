using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Result.Interfaces;

namespace CarMaintenanceTrackerServer.Services.UserService
{
    public interface IUserService
    {
        Task<IServiceResult<RegisterUserResponseDto>> RegisterUser(RegisterUserRequestDto user);
        Task<IServiceResult<LoginUserResponseDto>> LoginUser(LoginUserRequestDto user);
        Task<IServiceResult<GetUserResponseDto>> GetUserById(Guid userId);
        Task<IServiceResult<GetUserResponseDto>> GetUserByUsername(string username);
        Task<IServiceResult<UpdateUserResponseDto>> UpdateUser(Guid userId, UpdateUserRequestDto user);
        Task<IServiceResult<DeleteUserResponseDto>> DeleteUser(Guid userId);
    }
}
