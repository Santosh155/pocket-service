namespace pocket_service.DTOs.Auth
{
    public class AuthResponse
    {
         public string Token {get; set;} = null!;
         public string RefreshToken {get; set;} = null!;
         public DateTime ExpireDate {get; set;}
    }
}