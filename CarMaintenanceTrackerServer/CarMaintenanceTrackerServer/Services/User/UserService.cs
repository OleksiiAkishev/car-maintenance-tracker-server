using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.Services.User
{
    public class UserService : IUserService
    {
        public UserService()
        {
            
        }

        public async Task<UserLoginResponseDto> LoginUser(UserLoginRequestDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto user)
        {
            throw new NotImplementedException();
        }
    }
}
