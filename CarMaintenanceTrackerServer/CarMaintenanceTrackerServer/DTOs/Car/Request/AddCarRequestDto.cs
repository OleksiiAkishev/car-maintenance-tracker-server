using CarMaintenanceTrackerServer.DTOs.Maintenance.Request;
using CarMaintenanceTrackerServer.DTOs.User.Request;
using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.DTOs.Car.Request
{
    public class AddCarRequestDto
    {
        [Required]
        public required string Maker { get; set; }

        [Required]
        public required string Model { get; set; }

        [Required]
        public required int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        public required Guid UserId { get; set; }

        public required AddUserRequestDto User { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<AddLogMaintenanceRequestDto> MaintenanceLogs { get; set; } = [];
    }
}
