using System.ComponentModel.DataAnnotations;
using pocket_service.Models;

namespace pocket_service.DTOs.Auth
{
    public class ChangePasswordDto
    {
        [EmailAddress]
        public string Email {get; set;} = null!;
        public string OldPassword {get; set;} = null!;
        public string NewPassword {get; set;} = null!;
    }
}