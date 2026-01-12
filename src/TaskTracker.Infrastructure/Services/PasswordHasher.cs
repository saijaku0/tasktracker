using TaskTracker.Application.Common.Interfaces.Authentication;

namespace TaskTracker.Infrastructure.Services
{
    public class PasswordHasher
        : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}