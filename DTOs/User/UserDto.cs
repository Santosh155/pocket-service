using System;
namespace pocket_service.DTOs.User
{
    public class UserDto
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;

    }
}