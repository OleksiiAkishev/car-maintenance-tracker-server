using CarMaintenanceTrackerServer.DTOs.Maintenance.Response;
using CarMaintenanceTrackerServer.DTOs.User.Response;

namespace CarMaintenanceTrackerServer.DTOs.Car.Response
{
    public class UpdateCarResponseDto
    {
        public Guid Id { get; set; }

        public string Maker { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public required UpdateUserResponseDto User { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<UpdateLogMaintenanceResponseDto> MaintenanceLogs { get; set; } = [];
    }
}
