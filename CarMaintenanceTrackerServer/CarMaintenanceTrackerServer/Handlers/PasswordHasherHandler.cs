namespace CarMaintenanceTrackerServer.Handlers
{
    public class PasswordHasherHandler : IPasswordHasherHandler
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            throw new NotImplementedException();
        }
    }
}
