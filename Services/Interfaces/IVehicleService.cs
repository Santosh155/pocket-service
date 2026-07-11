using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> RegisterVehicle(Guid userId, Vehicle vehicle);
        Task<Vehicle> GetVehicleById(Guid id);
    }
}