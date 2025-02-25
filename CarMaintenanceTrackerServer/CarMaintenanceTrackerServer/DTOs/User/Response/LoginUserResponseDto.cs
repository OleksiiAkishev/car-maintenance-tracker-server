namespace CarMaintenanceTrackerServer.DTOs.User.Response
{
    public class LoginUserResponseDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
