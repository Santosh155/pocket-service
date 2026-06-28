using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public class IAddressService
    {
        Task<Address?> GetByAddressIdAsync(Guid id);
        Task<Address?> GetByAddressPostCodeAsync(string post);
        Task<Address> CreateAddressAsync(Address address);
        Task<IEnumerable<Address>> GetAllAddressAsync();
    }
}