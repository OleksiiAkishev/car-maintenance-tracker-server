using CarMaintenanceTrackerServer.DTOs.User.Request;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserRequestDto user)
        {
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserRequestDto user)
        {
            return Ok();
        }
    }
}
