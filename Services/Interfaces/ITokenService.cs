using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken (User user);
        string GenerateRefreshToken();
    }
}