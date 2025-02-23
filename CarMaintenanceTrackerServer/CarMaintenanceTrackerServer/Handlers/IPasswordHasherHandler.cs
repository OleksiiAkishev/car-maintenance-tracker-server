namespace CarMaintenanceTrackerServer.Handlers
{
    public interface IPasswordHasherHandler<in TUser> where TUser : class
    {
        string HashPassword(TUser user, string password);
        bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
    }
}
