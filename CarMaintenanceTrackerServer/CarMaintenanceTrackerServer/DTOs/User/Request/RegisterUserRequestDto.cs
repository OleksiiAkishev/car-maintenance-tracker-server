using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class RegisterUserRequestDto
    {
        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string EmailAddress { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
    }
}
