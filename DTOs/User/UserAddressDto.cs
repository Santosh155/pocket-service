using System;
namespace pocket_service.DTOs.User
{
    public class UserAddressDto
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string Role {get; set;} = null!;
        public string PhoneNumber {get; set;} = null!;
        public string Password {get; set;} = null!;
        public int? UnitNumber {get; set;}
        public int HouseNumber {get; set;} 
        public string StreetName {get; set;}  = null!;
        public string Suburb {get; set;} = null!;
        public int PostCode {get; set;}
        public string State {get; set;} = null!;
        public decimal Latitude {get; set;}
        public decimal Longitude {get; set;}

    }
}