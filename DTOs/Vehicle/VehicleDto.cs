namespace pocket_service.DTOs.Vehicle
{
    public class VehicleDto
    {
        public string VehicleType {get; set;} = null!;
        public string Make {get; set;} = null!;
        public string Model {get; set;} = null!;
        public int Year {get; set;}
        public string Vin {get; set;} = null!;
        public string LicensePlate {get; set;} = null!;
    }
}