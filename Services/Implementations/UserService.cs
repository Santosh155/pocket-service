using pocket_service.Models;
using pocket_service.Services.Interfaces;
using pocket_service.Services.InMemory;

namespace pocket_service.Services.Implementations
{
    public class UserService: IUserService
    {
        private InMemoryUserStore _store;
        public UserService(InMemoryUserStore store) => _store = store;
        public Task<User?> GetByIdAsync (Guid id)=>
            Task.FromResult(_store.Users.TryGetValue(id, out var u) ? u : null);
        public Task<User?> GetUserAsync (string email) =>
            Task.FromResult(_store.Users.Values.FirstOrDefault(u=>u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
        
        public Task<User> CreateAsync (User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _store.Users[user.Id] = user;
            return Task.FromResult(user);
        }
        
        public Task<IEnumerable<User>> GetAllAsync() =>
            Task.FromResult(_store.Users.Values.AsEnumerable());
    }
}