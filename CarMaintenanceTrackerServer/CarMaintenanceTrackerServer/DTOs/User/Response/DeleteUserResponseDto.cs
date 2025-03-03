namespace CarMaintenanceTrackerServer.DTOs.User.Response
{
    public class DeleteUserResponseDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
