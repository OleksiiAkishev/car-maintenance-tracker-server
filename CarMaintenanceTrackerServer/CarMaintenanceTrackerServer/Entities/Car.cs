using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Maker { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public int UserId { get; set; }

        [Required]
        public required User User { get; set; }

        public ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = [];   
    }
}
