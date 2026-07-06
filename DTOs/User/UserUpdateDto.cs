using pocket_service.Models;

namespace pocket_service.DTOs.User
{
    public class UserUpdateDto
    {
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? PhoneNumber {get; set;}
        public string? DrivingLicense {get; set;}
        public bool? EmailVerified {get; set;}
        public bool? EmailTokenUsed {get; set;}
        public bool? PasswordForgetTokenUsed {get; set;}
        public string? PasswordHash {get; set;}
        
    }
}