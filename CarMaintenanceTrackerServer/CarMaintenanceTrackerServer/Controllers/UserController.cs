using CarMaintenanceTrackerServer.DTOs.User.Request;
using CarMaintenanceTrackerServer.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService, ILogger logger) : ControllerBase
    {
        private readonly IUserService userService = userService;
        private readonly ILogger logger = logger;

        #region Constants
        private const string REGISTER_USER_ERROR_MESSAGE = "An error occurred while registering the user.";
        private const string LOGIN_USER_ERROR_MESSAGE = "An error occurred while logging in the user.";
        #endregion

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogWarning("Invalid register user request.");
                return BadRequest(ModelState);
            }
            try
            {
                var result = await this.userService.RegisterUser(user);
                if (!result.IsSuccess)
                {
                    this.logger.LogError(REGISTER_USER_ERROR_MESSAGE);
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("User registered successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, REGISTER_USER_ERROR_MESSAGE);
                return StatusCode(500, REGISTER_USER_ERROR_MESSAGE);
            }
        }

        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogWarning("Invalid login user request.");
                return BadRequest(ModelState);
            }
            try
            {
                var result = await this.userService.LoginUser(user);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("User login failed.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("User logged in successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, LOGIN_USER_ERROR_MESSAGE);
                return StatusCode(500, LOGIN_USER_ERROR_MESSAGE);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var result = await this.userService.GetUserById(userId);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("User not found.");
                    return NotFound(result);
                }
                this.logger.LogInformation("User found successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while getting the user by Id={userId}.";
                this.logger.LogError(ex, "An error occurred while getting the user by Id={UserId}", userId);
                return StatusCode(500, message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByUsername([FromQuery] string username) 
        {
            try
            {
                var result = await this.userService.GetUserByUsername(username);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("User not found.");
                    return NotFound(result);
                }
                this.logger.LogInformation("User found successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while getting the user by Username={username}";
                this.logger.LogError(ex, "An error occurred while getting the user by Username={Username}", username);
                return StatusCode(500, message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequestDto user)
        {
            if (!ModelState.IsValid)
            {
                this.logger.LogWarning("Invalid update user request.");
                return BadRequest(ModelState);
            }
            try
            {
                var result = await this.userService.UpdateUser(userId, user);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("User update failed.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("User updated successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while updating the user by Username={user?.Username}";
                this.logger.LogError(ex, "An error occurred while updating the user by Username={Username}", user?.Username);
                return StatusCode(500, message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId) 
        {
            try
            {
                var result = await this.userService.DeleteUser(userId);
                if (!result.IsSuccess)
                {
                    this.logger.LogError("User delete failed.");
                    return StatusCode(500, result);
                }
                this.logger.LogInformation("User deleted successfully.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = $"An error occurred while deleting the user by Id={userId}";
                this.logger.LogError(ex, "An error occurred while deleting the user by Id={UserId}", userId);
                return StatusCode(500, message);
            }
        }
    }
}
