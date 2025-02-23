using CarMaintenanceTrackerServer.DTOs.Car.Request;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class UpdateUserRequestDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
