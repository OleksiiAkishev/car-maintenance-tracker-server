using CarMaintenanceTrackerServer.DTOs.Car.Request;

namespace CarMaintenanceTrackerServer.DTOs.User.Request
{
    public class UpdateUserRequestDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
