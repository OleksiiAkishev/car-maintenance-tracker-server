using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class LoginUserRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
