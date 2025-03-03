using CarMaintenanceTrackerServer.Data.Entities;
using CarMaintenanceTrackerServer.Data.Repositories.UserRepository;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.DTOs.User.Response;
using CarMaintenanceTrackerServer.Handlers;
using CarMaintenanceTrackerServer.Mappers.UserMapper;
using CarMaintenanceTrackerServer.Result;
using CarMaintenanceTrackerServer.Services.UserService;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Moq;

namespace CarMaintenanceTrackerServerTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IPasswordHasherHandler<User>> passwordHasherHandlerMock;
        private readonly Mock<IUserMapper> userMapperMock;
        private readonly Mock<ILogger> loggerMock;
        private readonly UserService userService;

        public UserServiceTests()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.passwordHasherHandlerMock = new Mock<IPasswordHasherHandler<User>>();
            this.userMapperMock = new Mock<IUserMapper>();
            this.loggerMock = new Mock<ILogger>();
            this.userService = new UserService(this.userRepositoryMock.Object, this.passwordHasherHandlerMock.Object, this.userMapperMock.Object, this.loggerMock.Object);
        }

        [Fact]
        public async Task RegisterUser_WhenUserIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            RegisterUserRequestDto? user = null;

            // Act
            var result = await this.userService.RegisterUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.NULL_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("Provided user is null.", result?.Error?.Message);
        }

        [Fact]
        public async Task RegisterUser_WhenUserIsNotNull_ShouldReturnSuccessResult()
        {
            // Arrange
            var user = new RegisterUserRequestDto
            {
                Username = "testuser",
                Email = "testuser@domain",
                Password = "testpassword"
            };
            User userEntity = new()
            {
                Username = user.Username,
                Email = user.Email
            };
            var registeredUser = new RegisterUserResponseDto()
            {
                Username = userEntity.Username,
                Email = userEntity.Email
            };
            this.userMapperMock.Setup(m => m.MapRegisterUserRequestDtoToUser(user)).Returns(userEntity);
            this.passwordHasherHandlerMock.Setup(m => m.HashPassword(userEntity, user.Password)).Returns("hashedpassword");
            this.userRepositoryMock.Setup(m => m.RegisterUser(userEntity)).ReturnsAsync(userEntity);
            this.userMapperMock.Setup(m => m.MapUserToRegisterUserResponseDto(userEntity)).Returns(registeredUser);

            // Act
            var result = await this.userService.RegisterUser(user);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(user.Username, result?.Value?.Username);
        }

        [Fact]
        public async Task RegisterUser_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new RegisterUserRequestDto
            {
                Username = "testuser",
                Email = "testuser@domain",
                Password = "testpassword"
            };
            User userEntity = new()
            {
                Username = user.Username,
                Email = user.Email
            };
            this.userMapperMock.Setup(m => m.MapRegisterUserRequestDtoToUser(user)).Returns(userEntity);
            this.passwordHasherHandlerMock.Setup(m => m.HashPassword(userEntity, user.Password)).Returns("hashedpassword");
            this.userRepositoryMock.Setup(m => m.RegisterUser(userEntity)).ThrowsAsync(new Exception("An error occurred while registering the user."));

            // Act
            var result = await this.userService.RegisterUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.REGISTER_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while registering the user.", result?.Error?.Message);
        }

        [Fact]
        public async Task LoginUser_WhenUserIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            LoginUserRequestDto? user = null;

            // Act
            var result = await this.userService.LoginUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.NULL_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("Provided user is null.", result?.Error?.Message);
        }

        [Fact]
        public async Task LoginUser_WhenUserIsNotNullAndUserEntityIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new LoginUserRequestDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            User? userEntity = null;
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(user.Username)).ReturnsAsync(userEntity);

            // Act
            var result = await this.userService.LoginUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("User not found.", result?.Error?.Message);
        }

        [Fact]
        public async Task LoginUser_WhenUserIsNotNullAndUserEntityIsNotNullAndPasswordIsIncorrect_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new LoginUserRequestDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            User userEntity = new()
            {
                Username = user.Username,
                PasswordHash = "hashedpassword"
            };
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(user.Username)).ReturnsAsync(userEntity);
            this.passwordHasherHandlerMock.Setup(m => m.VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password)).Returns(false);

            // Act
            var result = await this.userService.LoginUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("Incorrect user credentials.", result?.Error?.Message);
        }

        [Fact]
        public async Task LoginUser_WhenUserIsNotNullAndUserEntityIsNotNullAndPasswordIsCorrect_ShouldReturnSuccessResult()
        {
            // Arrange
            var user = new LoginUserRequestDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            User userEntity = new()
            {
                Username = user.Username,
                PasswordHash = "hashedpassword"
            };
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(user.Username)).ReturnsAsync(userEntity);
            this.passwordHasherHandlerMock.Setup(m => m.VerifyHashedPassword(userEntity, userEntity.PasswordHash, user.Password)).Returns(true);
            var loginUserResponse = new LoginUserResponseDto()
            {
                Username = userEntity.Username
            };
            this.userMapperMock.Setup(m => m.MapUserToLoginUserResponseDto(userEntity)).Returns(loginUserResponse);

            // Act
            var result = await this.userService.LoginUser(user);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(user.Username, result?.Value?.Username);
        }

        [Fact]
        public async Task LoginUser_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new LoginUserRequestDto
            {
                Username = "testuser",
                Password = "testpassword"
            };
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(user.Username)).ThrowsAsync(new Exception("An error occurred while logging in the user."));

            // Act
            var result = await this.userService.LoginUser(user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.LOGIN_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while logging in the user.", result?.Error?.Message);
        }

        [Fact]
        public async Task GetUserById_WhenUserIsNotNull_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            User userEntity = new()
            {
                Id = userId,
                Username = "testuser",
                Email = "testuser@domain"
            };
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);
            var getUserResponse = new GetUserResponseDto()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email
            };
            this.userMapperMock.Setup(m => m.MapUserToGetUserResponse(userEntity)).Returns(getUserResponse);

            // Act
            var result = await this.userService.GetUserById(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userEntity.Id, result?.Value?.Id);
        }

        [Fact]
        public async Task GetUserById_WhenReturnedUserFromDatabaseIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            User? userEntity = null;
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);

            // Act
            var result = await this.userService.GetUserById(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("User not found.", result?.Error?.Message);
        }

        [Fact]
        public async Task GetUserById_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ThrowsAsync(new Exception("An error occurred while getting the user by user id."));

            // Act
            var result = await this.userService.GetUserById(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while getting the user by user id.", result?.Error?.Message);
        }

        [Fact]
        public async Task GetUserByUsername_WhenReturnedUserFromDatabaseIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            User? userEntity = null;
            var username = "testuser";
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(username)).ReturnsAsync(userEntity);

            // Act
            var result = await this.userService.GetUserById(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("User not found.", result?.Error?.Message);
        }

        [Fact]
        public async Task GetUserByUsername_WhenReturnedUserFromDatabaseIsNotNull_ShouldReturnSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            User userEntity = new()
            {
                Id = userId,
                Username = "testuser",
                Email = "testuser@domain"
            };
            var username = "testuser";
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(username)).ReturnsAsync(userEntity);
            var getUserResponse = new GetUserResponseDto()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email
            };
            this.userMapperMock.Setup(m => m.MapUserToGetUserResponse(userEntity)).Returns(getUserResponse);

            // Act
            var result = await this.userService.GetUserByUsername(username);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(userEntity.Id, result?.Value?.Id);
        }

        [Fact]
        public async Task GetUserByUsername_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var username = "testuser";
            this.userRepositoryMock.Setup(m => m.GetUserByUsername(username)).ThrowsAsync(new Exception("An error occurred while getting the user by username."));

            // Act
            var result = await this.userService.GetUserByUsername(username);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.FIND_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while getting the user by username.", result?.Error?.Message);
        }

        [Fact]
        public async Task UpdateUser_WhenReturnedUserFromDatabaseIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new UpdateUserRequestDto();
            Guid userId = Guid.NewGuid();
            User? userEntity = null;
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);

            // Act
            var result = await this.userService.UpdateUser(userId, user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("User not found.", result?.Error?.Message);
        }

        [Fact]
        public async Task UpdateUser_WhenReturnedUserFromDatabaseIsNotNullAndUserNameToBeUpdated_ShouldReturnSuccessResultAndUserEntityWithUpdatedUsername()
        {
            // Arrange
            var newUsername = "newusername";
            UpdateUserRequestDto updateUserRequestDto = new()
            {
                Username = newUsername
            };
            Guid userId = Guid.NewGuid();
            User userEntity = new () 
            { 
                Username = "oldusername",
                Email = "testuser@domain"
            };
            UpdateUserResponseDto updateUserResponseDto = new()
            {
                Username = newUsername,
                Email = "testuser@domain"
            };
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);
            this.userRepositoryMock.Setup(m => m.UpdateUser(userEntity)).ReturnsAsync(userEntity);
            this.userMapperMock.Setup(m => m.MapUserToUpdateUserResponseDto(userEntity)).Returns(updateUserResponseDto);

            // Act
            var result = await this.userService.UpdateUser(userId, updateUserRequestDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newUsername, result?.Value?.Username);
        }

        [Fact]
        public async Task UpdateUser_WhenReturnedUserFromDatabaseIsNotNullAndEmailToBeUpdated_ShouldReturnSuccessResultAndUserEntityWithUpdatedEmail()
        {
            // Arrange
            var newEmail = "newemail@domain";
            UpdateUserRequestDto updateUserRequestDto = new()
            {
                Email = newEmail
            };
            Guid userId = Guid.NewGuid();
            User userEntity = new()
            {
                Username = "testuser",
                Email = "oldemail@domain"
            };
            UpdateUserResponseDto updateUserResponseDto = new()
            {
                Username = "testuser",
                Email = newEmail
            };
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);
            this.userRepositoryMock.Setup(m => m.UpdateUser(userEntity)).ReturnsAsync(userEntity);
            this.userMapperMock.Setup(m => m.MapUserToUpdateUserResponseDto(userEntity)).Returns(updateUserResponseDto);

            // Act
            var result = await this.userService.UpdateUser(userId, updateUserRequestDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newEmail, result?.Value?.Email);
        }

        [Fact]
        public async Task UpdateUser_WhenReturnedUserFromDatabaseIsNotNullAndBothUsernameAndEmailToBeUpdated_ShouldReturnSuccessResultAndUserEntityWithUpdatedUsernameAndEmail()
        {
            // Arrange
            var newUsername = "newusername";
            var newEmail = "newemail@domain";
            UpdateUserRequestDto updateUserRequestDto = new()
            {
                Username = newUsername,
                Email = newEmail
            };
            Guid userId = Guid.NewGuid();
            User userEntity = new()
            {
                Username = "oldusername",
                Email = "oldemail@domain"
            };
            UpdateUserResponseDto updateUserResponseDto = new()
            {
                Username = newUsername,
                Email = newEmail
            };
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);
            this.userRepositoryMock.Setup(m => m.UpdateUser(userEntity)).ReturnsAsync(userEntity);
            this.userMapperMock.Setup(m => m.MapUserToUpdateUserResponseDto(userEntity)).Returns(updateUserResponseDto);

            // Act
            var result = await this.userService.UpdateUser(userId, updateUserRequestDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newUsername, result?.Value?.Username);
            Assert.Equal(newEmail, result?.Value?.Email);
        }

        [Fact]
        public async Task UpdateUser_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var user = new UpdateUserRequestDto();
            Guid userId = Guid.NewGuid();
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ThrowsAsync(new Exception("An error occurred while updating the user."));

            // Act
            var result = await this.userService.UpdateUser(userId, user);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.UPDATE_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while updating the user.", result?.Error?.Message);
        }

        [Fact]
        public async Task DeleteUser_WhenReturnedUserFromDatabaseIsNull_ShouldReturnFailureResult()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            User? userEntity = null;
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);

            // Act
            var result = await this.userService.DeleteUser(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.DELETE_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("User not found.", result?.Error?.Message);
        }

        [Fact]
        public async Task DeleteUser_WhenReturnedUserFromDatabaseIsNotNull_ShouldReturnSuccessResultAndIsDeletedSetToTrueForTheUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            User userEntity = new()
            {
                Id = userId,
                Username = "testuser",
                Email = "testuser@domain"
            };
            DeleteUserResponseDto deleteUserResponseDto = new()
            {
                Id = userId,
                Username = "testuser",
                Email = "testuser@domain",
                IsDeleted = true
            };
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ReturnsAsync(userEntity);
            this.userRepositoryMock.Setup(m => m.DeleteUser(userEntity)).ReturnsAsync(userEntity);
            this.userMapperMock.Setup(m => m.MapUserToDeleteUserResponseDto(userEntity)).Returns(deleteUserResponseDto);

            // Act
            var result = await this.userService.DeleteUser(userId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result?.Value?.IsDeleted);
        }

        [Fact]
        public async Task DeleteUser_WhenExceptionIsThrown_ShouldReturnFailureResult()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            this.userRepositoryMock.Setup(m => m.GetUserById(userId)).ThrowsAsync(new Exception("An error occurred while deleting the user."));

            // Act
            var result = await this.userService.DeleteUser(userId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrorDetailCodes.DELETE_USER_ERROR.GetDisplayName(), result?.Error?.Code);
            Assert.Equal("An error occurred while deleting the user.", result?.Error?.Message);
        }
    }
}
