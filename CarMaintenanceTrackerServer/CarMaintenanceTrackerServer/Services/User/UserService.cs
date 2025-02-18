using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Mappers.User;
using CarMaintenanceTrackerServer.Repositories.User;

namespace CarMaintenanceTrackerServer.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserMapper userMapper;

        public UserService(IUserRepository userRepository, IUserMapper userMapper)
        {
            this.userRepository = userRepository;
            this.userMapper = userMapper;
        }

        public Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserResponse> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserResponse> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponseDto> UpdateUser(UpdateUserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
