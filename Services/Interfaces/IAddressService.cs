using pocket_service.Models;

namespace pocket_service.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Address?> GetByAddressIdAsync(Guid id);
        Task<Address?> GetByAddressPostCodeAsync(int post);
        Task<Address> CreateAddressAsync(Address address);
        Task<IEnumerable<Address>> GetAllAddressAsync();
    }
}