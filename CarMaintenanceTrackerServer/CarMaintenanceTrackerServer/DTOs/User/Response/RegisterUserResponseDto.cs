﻿namespace CarMaintenanceTrackerServer.DTOs.User.Response
{
    public class RegisterUserResponseDto
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
