using pocket_service.Models;

namespace pocket_service.DTOs.Auth
{
    public class RefreshTokenRequest
    {
        public string RefreshToken {get; set;} = null!;
    }
}