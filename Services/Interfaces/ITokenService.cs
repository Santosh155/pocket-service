using pocket_service.Models;
using pocket_service.DTOs.Auth;

namespace pocket_service.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken (User user);
        string GenerateRefreshToken();
    }
}