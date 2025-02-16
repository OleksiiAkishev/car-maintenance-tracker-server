using CarMaintenanceTrackerServer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenanceTrackerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequestDto user)
        {
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequestDto user)
        {
            return Ok();
        }
    }
}
