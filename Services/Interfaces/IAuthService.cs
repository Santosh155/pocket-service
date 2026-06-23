using pocket_service.DTOs.Auth;

namespace pocket_service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync (RegisterRequest request, string ipAddress);
        Task<AuthResponse> LoginAsync (LoginRequest request, string ipAddress);
        Task<AuthResponse> RefreshTokenAsync (string token, string ipAddress);
        Task RevokeTokenAsync (string token, string ipAddress);
    }
}