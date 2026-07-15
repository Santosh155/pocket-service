using pocket_service.Services.Interfaces;
using pocket_service.Data;
using pocket_service.Models;
using Microsoft.EntityFrameworkCore;


namespace pocket_service.Services.Implementations
{
    public class VehicleService: IVehicleService
    {
        private readonly ApplicationDbContext _db;
        public VehicleService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Vehicle> RegisterVehicle(Guid id, Vehicle vehicle)
        {
            try
            {
                vehicle.UserId = id;
                _db.Vehicles.Add(vehicle);
                await _db.SaveChangesAsync();
                return vehicle;
            }catch(Exception ex)
            {
                throw new Exception("Failed to register", ex);
            }
        }

        public async Task<List<Vehicle>> GetVehicleById(Guid id)
        {
            return await _db.Vehicles.Where(v => v.UserId == id).ToListAsync();
        }

        public async Task<CarService> RequestVehicleService(CarService carService)
        {
            try
            {
                 _db.CarServices.Add(carService);
                await _db.SaveChangesAsync();
                return carService;
            }catch(Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message, ex);
            }
           
        }

        public async Task<IEnumerable<CarService>> GetAllCarService()
        {
            return await _db.CarServices.ToListAsync();
        }
    }
}