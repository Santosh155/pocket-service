using pocket_service.Models;
using pocket_service.Services.Interfaces;
using pocket_service.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace pocket_service.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly ApplicationDbContext _db;
        public UserService(ApplicationDbContext db) => _db = db;
        public Task<User?> GetByIdAsync (Guid id)=>
            _db.Users.FindAsync(id).AsTask();
        public Task<User?> GetUserAsync (string email) =>
            _db.Users.FirstOrDefaultAsync(u=>u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        
        public async Task<User> CreateAsync (User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _db.Users.ToListAsync();
    }
}