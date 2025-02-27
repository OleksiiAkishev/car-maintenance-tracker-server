using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public required string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        public ICollection<Car> Cars { get; set;} = [];
    }
}
