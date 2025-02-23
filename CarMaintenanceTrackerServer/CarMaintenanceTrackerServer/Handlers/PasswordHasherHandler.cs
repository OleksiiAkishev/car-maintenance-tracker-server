using Microsoft.AspNetCore.Identity;

namespace CarMaintenanceTrackerServer.Handlers
{
    public class PasswordHasherHandler<TUser> : IPasswordHasherHandler<TUser> where TUser : class
    {
        private readonly PasswordHasher<TUser> passwordHasher;
        public PasswordHasherHandler()
        {
            this.passwordHasher = new PasswordHasher<TUser>();
        }
        public string HashPassword(TUser user, string password)
        {
            return this.passwordHasher.HashPassword(user, password);
        }
        public bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            var result = this.passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
