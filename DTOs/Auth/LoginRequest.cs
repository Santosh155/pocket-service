using System.ComponentModel.DataAnnotations;

namespace pocket_service.DTOs.Auth
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}