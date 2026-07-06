using pocket_service.Models;
namespace pocket_service.Models
{
    public enum UserRole { User, Mechanic }
    public class User
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string PhoneNumber {get; set;} = null!;
        public string PasswordHash {get; set;} = null!;
        public string? DrivingLicense {get; set;}
        public int? EmailToken{get; set;}
        public bool EmailVerified {get; set;} = false;
        public bool EmailTokenUsed {get; set;} = false;
        public int? PasswordForgetToken {get; set;}
        public bool PasswordForgetTokenUsed {get; set;} = false;
        public UserRole Role {get; set;} = UserRole.User;
        public Guid AddressId {get; set;} 
        public Address? Address {get; set;}

    }
}