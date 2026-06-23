using pocket_service.Models;
using pocket_service.Services.InMemory;
using pocket_service.Services.Inetrfaces;

namespace pocket_service.Services.Implementations
{
    public class UserService: IUserService
    {
        private InMemoryUserStore _store;
        public UserService(InMemoryUserStore store) => _store = store;
        public Task<User?> GetByIdAsync (Guid id)=>
            Task.FromResult(_store.Users.TryGetValue(id, out var u) ? u : null);
        public Task<User?> GetByEmailAsync (string email) =>
            Task.FromResult(_store.Users.values.FirstOrDefault(u=>u.Email.Equals(email, StringComparision.OrdinalIgnoreCase)));
        
        public Task<User?> CreateAsync (User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _store.Users[user.Id] = user;
            return Task.FromResult(user);
        }
        
        public Task<IEnumerable<User>> GetAllAsync() =>
            Task.FromResult(_store.Users.Values.AsEnumerable());
    }
}