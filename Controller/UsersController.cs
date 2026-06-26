using Microsoft.AspNetCore.Mvc;
using pocket_service.DTOs.User;
using pocket_service.Services.Interfaces;

namespace pocket_service.UsersController
{
    [ApiController]
    [Route("api/user")]

    public class UserController: ControllerBase
    {
        private readonly IUserService _users;
        public UserController(IUserService users) => _users = users;

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
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {   
            var user = new pocket_service.Models.User
            {
                Email = dto.Email, 
                Role = Enum.Parse<pocket_service.Models.UserRole>(dto.Role)
            };
            var created = await _users.CreateAsync(user, dto.Password);
            return CreatedAtAction(nameof(Get), new {id = created.Id}, new UserDto {Id = created.Id, Email = created.Email});
        }
    }
}