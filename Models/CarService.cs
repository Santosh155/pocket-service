using pocket_service.Models;
namespace pocket_service.Models
{
    public enum ServiceStatus { Pending, InProgress, Completed, Cancelled }
    public class CarService
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string ServiceType {get; set;} = null;
        public string ServiceDescription {get; set;} = null;
        public DateTime ServiceDate {get; set;}
        public decimal ServiceCost {get; set;}
        public DateTime DateOfPayment {get; set;}
        public ServiceStatus ServiceStatus {get; set;} = ServiceStatus.Pending;
        public Guid VehicleId {get; set;}
        public Vehicle? Vehicle {get; set;}
        public Guid MechanicId {get; set;}
        public Mechanic? Mechanic {get; set;}
        public Guid AddressId {get; set;}
        public Address? Address {get; set;}
    }
}