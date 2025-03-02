using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Mappers.UserMapper
{
    public interface IUserMapper
    {
        User MapRegisterUserRequestDtoToUser(RegisterUserRequestDto user);
        RegisterUserResponseDto MapUserToRegisterUserResponseDto(User user);
        LoginUserResponseDto MapUserToLoginUserResponseDto(User user);
        GetUserResponseDto MapUserToGetUserResponse(User user);
        UpdateUserResponseDto MapUserToUpdateUserResponseDto(User user);
    }
}
