using pocket_service.Models;
namespace pocket_service.Models
{
    public class Vehicle
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string VehicleType {get; set;} = null;
        public string Make {get; set;} = null;
        public string Model {get; set;} = null;
        public int Year {get; set;}
        public string Vin {get; set;} = null;
        public string LicensePlate {get; set;} = null;
        public Guid UserId {get; set;}
        public User? User {get; set;}
    }
}