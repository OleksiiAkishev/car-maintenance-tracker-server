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
                this.logger.LogError("Requested register user is null.");
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
            if (user == null)
            {
                this.logger.LogError("Requested login user is null.");
                return ResultFactory.CreateFailureResult<LoginUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.NULL_USER_ERROR.GetDisplayName()}", "Provided user is null.")).Value ?? new LoginUserResponseDto();
            }
            try
            {
                var userEntity = await userRepository.GetUserByUsername(user.Username);
                if (userEntity == null) 
                {
                    this.logger.LogError("User with the \"username=\"{UserName} was not found.", user.Username);
                    return ResultFactory.CreateFailureResult<LoginUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName()}", "User not found.")).Value ?? new LoginUserResponseDto();
                }
                if (!this.passwordHasherHandler.VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password))
                {
                    this.logger.LogError("User with the \"username=\"{UserName} provided an incorrect password.", user.Username);
                    return ResultFactory.CreateFailureResult<LoginUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName()}", "Incorrect password.")).Value ?? new LoginUserResponseDto();
                }
                return ResultFactory.CreateSuccessResult(userMapper.MapUserToLoginUserResponseDto(userEntity)).Value;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while logging in the user.");
                return ResultFactory.CreateFailureResult<LoginUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value ?? new LoginUserResponseDto();
            }
        }

        public async Task<GetUserResponse> GetUserById(Guid userId)
        {
            try 
            {
                var userEntity = await this.userRepository.GetUserById(userId);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"userId=\"{UserId} was not found.", userId);
                    return ResultFactory.CreateFailureResult<GetUserResponse>(new ErrorDetails($"{ErrorDetailCodes.FIND_USER_ERROR.GetDisplayName()}", "User not found.")).Value ?? new GetUserResponse();
                }
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToGetUserResponse(userEntity)).Value;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while getting the user.");
                return ResultFactory.CreateFailureResult<GetUserResponse>(new ErrorDetails($"{ErrorDetailCodes.FIND_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value ?? new GetUserResponse();
            }
        }

        public async Task<GetUserResponse> GetUserByUsername(string username)
        {
            try
            {
                var userEntity = await this.userRepository.GetUserByUsername(username);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"username=\"{UserName} was not found.", username);
                    return ResultFactory.CreateFailureResult<GetUserResponse>(new ErrorDetails($"{ErrorDetailCodes.FIND_USER_ERROR.GetDisplayName()}", "User not found.")).Value ?? new GetUserResponse();
                }
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToGetUserResponse(userEntity)).Value;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while getting the user.");
                return ResultFactory.CreateFailureResult<GetUserResponse>(new ErrorDetails($"{ErrorDetailCodes.FIND_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value ?? new GetUserResponse();
            }
        }

        public async Task<UpdateUserResponseDto> UpdateUser(Guid userId, UpdateUserRequestDto user)
        {
            try
            {
                var userEntity = await this.userRepository.GetUserById(userId);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"userId=\"{UserId} was not found.", userId);
                    return ResultFactory.CreateFailureResult<UpdateUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName()}", "User not found.")).Value ?? new UpdateUserResponseDto();
                }
                if (!string.IsNullOrEmpty(user.Username) && user.Username != userEntity.Username)
                {
                    userEntity.Username = user.Username;
                }
                if (!string.IsNullOrEmpty(user.Email) && user.Email != userEntity.Email)
                {
                    userEntity.Email = user.Email;
                }
                var updatedUser = await this.userRepository.UpdateUser(userEntity);
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToUpdateUserResponseDto(updatedUser)).Value;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while updating the user.");
                return ResultFactory.CreateFailureResult<UpdateUserResponseDto>(new ErrorDetails($"{ErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value ?? new UpdateUserResponseDto();
            }
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                var deletedUserResult = await this.userRepository.DeleteUser(userId);
                return ResultFactory.CreateSuccessResult(deletedUserResult).Value;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while deleting the user.");
                return ResultFactory.CreateFailureResult<bool>(new ErrorDetails($"{ErrorDetailCodes.DELETE_USER_ERROR.GetDisplayName()}", $"{ex.Message}", $"{ex.StackTrace}")).Value;
            }
        }
    }
}
