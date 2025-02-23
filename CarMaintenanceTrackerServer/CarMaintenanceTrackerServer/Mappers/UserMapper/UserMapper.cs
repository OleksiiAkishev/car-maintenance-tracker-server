using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Mappers.UserMapper
{
    public class UserMapper : IUserMapper
    {
        public User MapRegisterUserRequestDtoToUser(RegisterUserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public LoginUserResponseDto MapUserToLoginUserResponseDto(User user)
        {
            throw new NotImplementedException();
        }

        public RegisterUserResponseDto MapUserToRegisterUserResponseDto(User user)
        {
            throw new NotImplementedException();
        }
    }
}
