using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class AddUserRequestDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
