using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> RegisterVehicle(Guid userId, Vehicle vehicle);
        Task<List<Vehicle>> GetVehicleById(Guid id);
        Task<CarService> RequestVehicleService(CarService carService);
        Task<IEnumerable<CarService>> GetAllCarService();
    }
}