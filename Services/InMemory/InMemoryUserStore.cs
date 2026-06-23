using System.Collections.Concurrent;
using System.Reflection.Metadata;
using pocket_service.Models;

namespace pokcet_service.Services.InMemory
{
    public class InMemoryUserStore
    {
        internal readonly ConcurrentDictionary<Guid, User> Users = new();
        internal readonly ConcurrentDictionary<string, RefreshToken> RefreshTokens = new();

        public InMemoryUserStore()
        {
            var admin = new UserStringHandle
            {
                Email = "admin@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPasswrod("Admin123!"),
                Role = "User",
                EmailConfirmed = true
            };
            Users[admin.Id] = admin;
        }
    }
}