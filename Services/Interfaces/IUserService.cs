using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync (int id);
        Task<User?> GetUserAsync (string email);
        Task<User> CreateAsync (User user, string password);
        Task<IEnumerable> GetAllAsync();
    }
}