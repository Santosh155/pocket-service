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
        public async Task<User?> GetUserAsync (string email)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(email)) return null;
                var normalize = email.Trim().ToLower();
                return await _db.Users.FirstOrDefaultAsync(u=>u.Email.ToLower() == normalize);
            } catch (Exception ex)
            {
                throw new Exception($"Error retrieving user by email: {ex.Message}", ex);
            }
        }
        
        public async Task<User> CreateAsync (User user, string password)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UserUpdateAsync(Guid Id, User user)
        {
            var existingUser = await _db.Users.FindAsync(user.Id) 
                ?? throw new KeyNotFoundException("User not found");
            if(user.FirstName != null) existingUser.FirstName = user.FirstName;
            if(user.LastName != null) existingUser.LastName = user.LastName;
            if(user.PhoneNumber != null) existingUser.PhoneNumber = user.PhoneNumber;

            // will update later 
            // if(user.DrivingLicense != null) existingUser.DrivingLicense = user.DrivingLicense;
            // if(user.EmailToken.HasValue) existingUser.EmailToken = user.EmailToken.Value;
            // if(user.EmailVerified.HasValue) existingUser.EmailVerified = user.EmailVerified.Value;
            // if(user.EmailTokenUsed.HasValue) existingUser.EmailTokenUsed = user.EmailTokenUsed.Value;
            // if(user.PasswordForgetToken.HasValue) existingUser.PasswordForgetToken = user.PasswordForgetToken.Value;
            // if(user.PasswordForgetTokenUsed.HasValue) existingUser.PasswordForgetTokenUsed = user.PasswordForgetTokenUsed.Value;

            await _db.SaveChangesAsync();
            return existingUser;
        }
        
        public async Task<User> UserEmailVerify(Guid Id, int token)
        {
            var existingUser = await _db.Users.FindAsync(Id) 
                ?? throw new KeyNotFoundException("User not found");
            if(token != existingUser.EmailToken) throw new UnauthorizedAccessException("Invalid email token");
            
            existingUser.EmailVerified = true;
            existingUser.EmailTokenUsed = true;

            await _db.SaveChangesAsync();
            return existingUser;
        }

        public async Task ChangePasswordAsync(Guid Id, string NewPassword)
        {
            var existingUser = await _db.Users.FindAsync(Id) ?? throw new Exception("User not found");
            var newPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            existingUser.PasswordHash = newPassword;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _db.Users.ToListAsync();
    }
}