﻿using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Handlers;
using CarMaintenanceTrackerServer.Mappers.UserMapper;
using CarMaintenanceTrackerServer.Result;
using CarMaintenanceTrackerServer.Result.Interfaces;
using Microsoft.OpenApi.Extensions;

namespace CarMaintenanceTrackerServer.Services.UserService
{
    public class UserService(IUserRepository userRepository, IPasswordHasherHandler<User> passwordHasherHandler, IUserMapper userMapper, ILogger logger) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IPasswordHasherHandler<User> passwordHasherHandler = passwordHasherHandler;
        private readonly IUserMapper userMapper = userMapper;
        private readonly ILogger logger = logger;

        public async Task<IServiceResult<RegisterUserResponseDto>> RegisterUser(RegisterUserRequestDto user)
        {
            if (user == null)
            {
                this.logger.LogError("Requested register user is null.");
                return ResultFactory.CreateFailureResult<RegisterUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.NULL_USER_ERROR.GetDisplayName(), "Provided user is null."));
            }
            try
            {
                var userEntity = userMapper.MapRegisterUserRequestDtoToUser(user);
                userEntity.PasswordHash = this.passwordHasherHandler.HashPassword(userEntity, user.Password);
                var registeredUser = await userRepository.RegisterUser(userEntity);
                var successResult = ResultFactory.CreateSuccessResult(userMapper.MapUserToRegisterUserResponseDto(registeredUser));
                return successResult;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while registering the user.");
                return ResultFactory.CreateFailureResult<RegisterUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.REGISTER_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }

        public async Task<IServiceResult<LoginUserResponseDto>> LoginUser(LoginUserRequestDto user)
        {
            if (user == null)
            {
                this.logger.LogError("Requested login user is null.");
                return ResultFactory.CreateFailureResult<LoginUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.NULL_USER_ERROR.GetDisplayName(), "Provided user is null."));
            }
            try
            {
                var userEntity = await userRepository.GetUserByUsername(user.Username);
                if (userEntity == null) 
                {
                    this.logger.LogError("User with the \"username=\"{UserName} was not found.", user.Username);
                    return ResultFactory.CreateFailureResult<LoginUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), "User not found."));
                }
                if (!this.passwordHasherHandler.VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password))
                {
                    this.logger.LogError("User with the \"username=\"{UserName} provided incorrect credentials.", user.Username);
                    return ResultFactory.CreateFailureResult<LoginUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), "Incorrect user credentials."));
                }
                return ResultFactory.CreateSuccessResult(userMapper.MapUserToLoginUserResponseDto(userEntity));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while logging in the user.");
                return ResultFactory.CreateFailureResult<LoginUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }

        public async Task<IServiceResult<GetUserResponseDto>> GetUserById(Guid userId)
        {
            try 
            {
                var userEntity = await this.userRepository.GetUserById(userId);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"userId=\"{UserId} was not found.", userId);
                    return ResultFactory.CreateFailureResult<GetUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), "User not found."));
                }
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToGetUserResponse(userEntity));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while getting the user by user id.");
                return ResultFactory.CreateFailureResult<GetUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }

        public async Task<IServiceResult<GetUserResponseDto>> GetUserByUsername(string username)
        {
            try
            {
                var userEntity = await this.userRepository.GetUserByUsername(username);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"username=\"{UserName} was not found.", username);
                    return ResultFactory.CreateFailureResult<GetUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), "User not found."));
                }
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToGetUserResponse(userEntity));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while getting the user by username.");
                return ResultFactory.CreateFailureResult<GetUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }

        public async Task<IServiceResult<UpdateUserResponseDto>> UpdateUser(Guid userId, UpdateUserRequestDto user)
        {
            try
            {
                var userEntity = await this.userRepository.GetUserById(userId);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"userId=\"{UserId} was not found.", userId);
                    return ResultFactory.CreateFailureResult<UpdateUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName(), "User not found."));
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
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToUpdateUserResponseDto(updatedUser));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while updating the user.");
                return ResultFactory.CreateFailureResult<UpdateUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }

        public async Task<IServiceResult<DeleteUserResponseDto>> DeleteUser(Guid userId)
        {
            try
            {
                var userEntity = await this.userRepository.GetUserById(userId);
                if (userEntity == null)
                {
                    this.logger.LogError("User with the \"userId=\"{UserId} was not found.", userId);
                    return ResultFactory.CreateFailureResult<DeleteUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.DELETE_USER_ERROR.GetDisplayName(), "User not found."));
                }
                var deletedUserResult = await this.userRepository.DeleteUser(userEntity);
                return ResultFactory.CreateSuccessResult(this.userMapper.MapUserToDeleteUserResponseDto(deletedUserResult));
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "An error occurred while deleting the user.");
                return ResultFactory.CreateFailureResult<DeleteUserResponseDto>(ResultFactory.CreateErrorDetails(UserErrorDetailCodes.DELETE_USER_ERROR.GetDisplayName(), ex.Message, ex.StackTrace));
            }
        }
    }
}
