using pocket_service.DTOs.Auth;
using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IAuthService
    {
        // Task<AuthResponse> RegisterAsync (RegisterRequest request, string ipAddress);
        // Task<AuthResponse> LoginAsync (LoginRequest request, string ipAddress);
         Task<RefreshToken> SaveRefreshTokenAsync (RefreshToken token);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken, string? ipAddress);
        // Task RevokeTokenAsync (string token, string ipAddress);
    }
}