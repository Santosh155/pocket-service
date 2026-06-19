namespace pocket_service.Models
{
    public class Address
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public int UnitNumber {get; set;}
        public int HouseNumber {get; set;}
        public string StreetName {get; set;} = null;
        public int PostCode {get; set;}
        public string Suburb {get; set;} = null;
        public string State {get; set;} = null;
        public int Latitude {get; set;}
        public int Longitude {get; set;}
    }
}