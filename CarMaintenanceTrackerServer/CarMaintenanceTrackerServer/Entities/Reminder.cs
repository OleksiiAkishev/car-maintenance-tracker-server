using System.ComponentModel.DataAnnotations;

namespace CarMaintenanceTrackerServer.Entities
{
    public class Reminder
    {
        public int Id { get; set; }

        public DateTime ReminderDate { get; set; }

        public int Mileage { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public int CarId { get; set; }

        [Required]
        public required Car Car { get; set; }
    }
}