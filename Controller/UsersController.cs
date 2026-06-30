using Microsoft.AspNetCore.Mvc;
using pocket_service.DTOs.User;
using pocket_service.DTOs.Auth;
using pocket_service.Models;
using pocket_service.Services.Interfaces;

namespace pocket_service.UsersController
{
    [ApiController]
    [Route("api/user")]

    public class UserController: ControllerBase
    {
        private readonly IUserService _users;
        private readonly IAddressService _address;
        public UserController(IUserService users, IAddressService address)
        {
            _users = users;
            _address = address;
        }
            

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _users.GetAllAsync();
            var dtos = list.Select(u => new UserDto{Id = u.Id, Email = u.Email, });
            return Ok(dtos);
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> Get(Guid id)
        {
            var u = await _users.GetByIdAsync(id);
            if (u == null) return NotFound();
            return Ok(new UserDto {Id = u.Id, Email = u.Email});

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddressDto dto)
        {
            try
            {
                var checkEmail = await _users.GetUserAsync(dto.Email);
                if (checkEmail != null)
                    return Conflict(new {message = "Email already in use"});
                
                var address = new pocket_service.Models.Address
                {
                    UnitNumber = dto.UnitNumber ?? 0,
                    HouseNumber = dto.HouseNumber, 
                    StreetName = dto.StreetName, 
                    Suburb = dto.Suburb, 
                    PostCode = dto.PostCode, 
                    State = dto.State, 
                    Latitude = dto.Latitude, 
                    Longitude = dto.Longitude
                };
                var addressCreated = await _address.CreateAddressAsync(address);
                var user = new pocket_service.Models.User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email, 
                    Role = Enum.Parse<pocket_service.Models.UserRole>(dto.Role),
                    PhoneNumber = dto.PhoneNumber,
                    AddressId = addressCreated.Id
                };
                var created = await _users.CreateAsync(user, dto.Password);
                return CreatedAtAction(nameof(Get), 
                    new {id = created.Id}, 
                    new UserAddressDto 
                    { 
                        Id = created.Id,
                        Email = created.Email,
                        FirstName = created.FirstName,
                        LastName = created.LastName,
                        Role = created.Role.ToString(),
                        PhoneNumber = created.PhoneNumber,
                    
                        UnitNumber = addressCreated.UnitNumber,
                        HouseNumber = addressCreated.HouseNumber,
                        StreetName = addressCreated.StreetName,
                        Suburb = addressCreated.Suburb,
                        PostCode = addressCreated.PostCode,
                        State = addressCreated.State,
                        Latitude = addressCreated.Latitude,
                        Longitude = addressCreated.Longitude        
                        }
                    );
        }
        catch(Exception ex){
            return Conflict(new {message =  ex.Message});
        }
        }
        
    }
}