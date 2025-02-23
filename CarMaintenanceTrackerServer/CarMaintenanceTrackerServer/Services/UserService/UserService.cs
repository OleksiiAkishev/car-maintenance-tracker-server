﻿using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Handlers;
using CarMaintenanceTrackerServer.Mappers.UserMapper;
using CarMaintenanceTrackerServer.Result;
using Microsoft.OpenApi.Extensions;

namespace CarMaintenanceTrackerServer.Services.UserService
{
    public class UserService(IUserRepository userRepository, IPasswordHasherHandler<User> passwordHasherHandler, IUserMapper userMapper, ILogger logger) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IPasswordHasherHandler<User> passwordHasherHandler = passwordHasherHandler;
        private readonly IUserMapper userMapper = userMapper;
        private readonly ILogger logger = logger;

        public async Task<RegisterUserResponseDto> RegisterUser(RegisterUserRequestDto user)
        {
            if (user == null)
            {
                return ResultFactory.CreateFailureResult<RegisterUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.NULL_USER_ERROR.GetDisplayName()}", "Provided user is null.")).Value ?? new RegisterUserResponseDto();
            }
            try
            {
                var userEntity = userMapper.MapRegisterUserRequestDtoToUser(user);
                userEntity.PasswordHash = this.passwordHasherHandler.HashPassword(userEntity, user.Password);
                var registeredUser = await userRepository.RegisterUser(userEntity);
                var successResult = ResultFactory.CreateSuccessResult(userMapper.MapUserToRegisterUserResponseDto(registeredUser)).Value;
                return successResult;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while registering the user.");
                return ResultFactory.CreateFailureResult<RegisterUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.REGISTER_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value ?? new RegisterUserResponseDto();
            }
        }

        public async Task<LoginUserResponseDto> LoginUser(LoginUserRequestDto user)
        {
            if (user != null)
            {

            }
            await userRepository.LoginUser(1);
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
