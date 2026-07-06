using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync (Guid id);
        Task<User?> GetUserAsync (string email);
        Task<User> CreateAsync (User user, string password);
        Task<User> UserUpdateAsync(Guid Id, User user);
        Task<User> UserEmailVerify(Guid Id, int token);
        Task<IEnumerable<User>> GetAllAsync();
    }
}