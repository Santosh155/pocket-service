namespace pocket_service.DTOs.Auth
{
    public class RegisterRequest
    {
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string PhoneNumber {get; set;} = null!;
        public string Password {get; set;} = null!;
        public string? DrivingLicense {get; set;}
        public AddressDto? Address {get; set;}

    }
}