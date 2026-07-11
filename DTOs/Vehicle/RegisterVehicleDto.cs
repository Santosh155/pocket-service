namespace pocket_service.DTOs.Vehicle
{
    public class RegisterVehicleDto
    {
        public string ServiceType {get; set;} = null!;
        public string ServiceDescription {get; set;} = null!;
        public DateTime? ServiceDate {get; set;}
        public int? UnitNumber {get; set;}
        public int HouseNumber {get; set;}
        public string? StreetName {get; set;}
        public int PostCode {get; set;}
        public string? Suburb {get; set;}
        public string? State {get; set;}
        public decimal Longitude {get; set;}
        public decimal Latitude {get; set;}
    }
}