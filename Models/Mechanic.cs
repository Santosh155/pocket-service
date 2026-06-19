using pocket_service.Models;
namespace pocket_service.Models
{
    public class Mechanic
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string MechanicLicense {get; set;} = null;
        public bool MechanicVerified {get; set;} = false;
        public Guid UserId {get; set;} 
        public User? User {get; set;}
    }
}