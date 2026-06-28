using System;
namespace pocket_service.DTOs.User
{
    public class UserDto
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string Role {get; set;} = null!;
        public string PhoneNumber {get; set;} = null!;
        public string Password {get; set;} = null!;

    }
}