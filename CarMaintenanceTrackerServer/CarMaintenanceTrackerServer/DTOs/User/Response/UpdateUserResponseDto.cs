using CarMaintenanceTrackerServer.DTOs.Car.Request;

namespace CarMaintenanceTrackerServer.DTOs.User.Response
{
    public class UpdateUserResponseDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
