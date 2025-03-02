using CarMaintenanceTrackerServer.DTOs.Car.Response;

namespace CarMaintenanceTrackerServer.DTOs.User.Response
{
    public class GetUserResponseDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public IEnumerable<GetCarResponseDto> Cars { get; set; } = [];
    }
}
