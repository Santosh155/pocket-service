namespace pocket_service.Models
{
    public class RefreshToken
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public Guid UserId {get; set;}
        public User? User {get; set;}
        public string Token {get; set;} = null!;
        public DateTime ExpireDate {get; set;}
        public DateTime? RevokeAt {get; set;}
        public string? ReplacedByToken {get; set;}
        public string? RemoteIp {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
        public bool IsActive => RevokeAt == null && DateTime.UtcNow < ExpireDate;
    }
}