using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Mappers.UserMapper
{
    public class UserMapper : IUserMapper
    {
        public User MapRegisterUserRequestDtoToUser(RegisterUserRequestDto user)
        {
            return new User
            {
                Username = user.Username,
                Email = user.Email
            };
        }

        public GetUserResponseDto MapUserToGetUserResponse(User user)
        {
            return new GetUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public LoginUserResponseDto MapUserToLoginUserResponseDto(User user)
        {
            return new LoginUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public RegisterUserResponseDto MapUserToRegisterUserResponseDto(User user)
        {
            return new RegisterUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public UpdateUserResponseDto MapUserToUpdateUserResponseDto(User user)
        {
            return new UpdateUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
