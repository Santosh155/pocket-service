using System.Collections.Concurrent;
using System.Reflection.Metadata;
using BCrypt.Net;
using pocket_service.Models;

namespace pocket_service.Services.InMemory
{
    public class InMemoryUserStore
    {
        internal readonly ConcurrentDictionary<Guid, User> Users = new();
        internal readonly ConcurrentDictionary<string, RefreshToken> RefreshTokens = new();

        public InMemoryUserStore()
        {
            var admin = new User
            {
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = UserRole.User,
                EmailVerified = true
            };
            Users[admin.Id] = admin;
        }
    }
}