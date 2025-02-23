﻿using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Handlers;
using CarMaintenanceTrackerServer.Mappers.UserMapper;
using CarMaintenanceTrackerServer.Result;

namespace CarMaintenanceTrackerServer.Services.User
{
    public class UserService(IUserRepository userRepository, IPasswordHasherHandler passwordHasherHandler, IUserMapper userMapper) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IPasswordHasherHandler passwordHasherHandler = passwordHasherHandler;
        private readonly IUserMapper userMapper = userMapper;

        public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto user)
        {
            var result = new RegisterUserResponseDto();
            if (user != null) 
            {
                var userEntity = this.userMapper.MapRegisterUserRequestDtoToUser(user);
                var registeredUser = await this.userRepository.RegisterUser(userEntity);
                if (registeredUser != null) 
                {
                    var successResult = ResultFactory.CreateSuccessResult(this.userMapper.MapUserToRegisterUserResponseDto(registeredUser)).Value;
                    return successResult;
                }
            }
            return result;
        }

        public async Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto user)
        {
            if (user != null) 
            {

            }
            await this.userRepository.LoginUser(1);
            return new LoginUserResponseDto();
        }

        public async Task<GetUserResponse> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserResponse> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateUserResponseDto> UpdateUser(UpdateUserRequestDto user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        //private bool IsRequestUserValid(T user) where T : class
        //{
        //    return user != null;
        //}
    }
}
