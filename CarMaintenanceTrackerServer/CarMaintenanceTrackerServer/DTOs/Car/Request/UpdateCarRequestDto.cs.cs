using CarMaintenanceTrackerServer.DTOs.Maintenance.Request;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.Car.Request
{
    public class UpdateCarRequestDto
    {
        public string Maker { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required UpdateUserRequestDto User { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<UpdateLogMaintenanceRequestDto> MaintenanceLogs { get; set; } = [];
    }
}
