namespace pocket_service.DTOs
{
    public class AddressDto
    {
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