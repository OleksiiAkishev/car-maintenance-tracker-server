using CarMaintenanceTrackerServer.DTOs.User.Response;
using UserEntity = CarMaintenanceTrackerServer.Entities.User;

namespace CarMaintenanceTrackerServer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        public Task<UserRegisterResponseDto> RegisterUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<UserLoginResponseDto> LoginUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
