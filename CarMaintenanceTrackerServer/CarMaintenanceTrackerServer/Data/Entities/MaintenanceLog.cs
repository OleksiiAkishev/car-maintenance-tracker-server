using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.Data.Entities
{
    public class MaintenanceLog
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? ServiceType { get; set; }

        public int Mileage { get; set; }

        public string? Notes { get; set; }

        public Guid CarId { get; set; }

        [Required]
        public required Car Car { get; set; }
    }
}
