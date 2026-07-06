using System.ComponentModel.DataAnnotations;
using pocket_service.Models;

namespace pocket_service.DTOs.Auth
{
    public class VerifyEmailDto
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;} = null!;
        [Required]
        public int EmailToken {get; set;}
    }
}