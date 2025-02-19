using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class RegisterUserRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
