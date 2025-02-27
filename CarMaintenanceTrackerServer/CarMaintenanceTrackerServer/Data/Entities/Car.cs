using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.Data.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Maker { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        [Required]
        public required User User { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = [];   
    }
}
