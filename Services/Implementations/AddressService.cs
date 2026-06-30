using pocket_service.Models;
using pocket_service.Services.Interfaces;
using pocket_service.Data;
using Microsoft.EntityFrameworkCore;

namespace pocket_service.Services.Implementations
{
    public class AddressService: IAddressService
    {
        private readonly ApplicationDbContext _db;
        public AddressService(ApplicationDbContext db) => _db = db;
        public Task<Address?> GetByAddressIdAsync(Guid id) =>
            _db.Address.FindAsync(id).AsTask();

        public Task<Address?> GetByAddressPostCodeAsync(int post) =>
            _db.Address.FirstOrDefaultAsync(u=> u.PostCode == post);
        
        public async Task<Address> CreateAddressAsync(Address address)
        {
            _db.Address.Add(address);
            await _db.SaveChangesAsync();
            return address;
        }

        public async Task<IEnumerable<Address>> GetAllAddressAsync() =>
            await _db.Address.ToListAsync();
        
    }
}