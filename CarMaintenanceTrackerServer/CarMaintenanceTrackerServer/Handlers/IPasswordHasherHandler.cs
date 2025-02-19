namespace CarMaintenanceTrackerServer.Handlers
{
    public interface IPasswordHasherHandler
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
