using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using pocket_service.DTOs.User;
using pocket_service.DTOs.Auth;
using pocket_service.Models;
using pocket_service.Services.Interfaces;
using System.Data;
using System.Net.Http.Json;

namespace pocket_service.UsersController
{
    [ApiController]
    [Route("api/user")]

    public class UserController: ControllerBase
    {
        private readonly IUserService _users;
        private readonly IAddressService _address;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public UserController(
            IUserService users, IAddressService address, 
            ITokenService tokenService, IAuthService authService, IConfiguration config)
        {
            _users = users;
            _address = address;
            _tokenService = tokenService;
            _config = config;
            _authService = authService;
        }
            
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _users.GetAllAsync();
            var dtos = list.Select(u => new UserDto{Id = u.Id, Email = u.Email, });
            return Ok(dtos);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
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
                
                var address = new Address
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
                int token = RandomNumberGenerator.GetInt32(100000, 999999);
                // var response = await _users.UserEmailVerify(res.Id, token);
                var user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email, 
                    Role = Enum.Parse<UserRole>(dto.Role),
                    PhoneNumber = dto.PhoneNumber,
                    AddressId = addressCreated.Id,
                    EmailToken = token
                };
                var created = await _users.CreateAsync(user, dto.Password);
                if(dto.Role == "Mechanic")
                {
                   await _users.CreateMechanicAsync(created.Id);
                }
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
                return Conflict(new {message =  ex.Message, stackTrace = ex.StackTrace});
            }
        }

        [HttpPatch("verify-email-token")]
        public async Task<IActionResult> VerifyEmailToken([FromBody] VerifyEmailDto req)
        {
            try
            {
                var res = await _users.GetUserAsync(req.Email);
                if(res == null)
                    return NotFound(new {message = "User not found"});
                var response = await _users.UserEmailVerify(res.Id, req.EmailToken);
                return Ok(new { message = "Email verified successfully "});
            }
            catch (Exception ex)
            {
                return Conflict(new {message = ex.Message, stackTrace = ex.StackTrace});
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            try
            {
                var user = await _users.GetUserAsync(req.Email.Trim().ToLower());
                if(user == null){
                    return Unauthorized("Invalid email or password");
                }
                if(!BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                   return Unauthorized("Invalid email or password");
                var token = _tokenService.GenerateAccessToken(user);
                var refreshToken = _tokenService.GenerateRefreshToken();
                
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    ExpireDate = DateTime.UtcNow.AddDays(double.Parse(_config["jwt:RefreshTokenLifetimeDays"]!))
                };
                var authResponse = await _authService.SaveRefreshTokenAsync(refreshTokenEntity);
                return Ok(new 
                {
                    AccessToken = token, 
                    RefreshToken = authResponse.Token, 
                    expireDate = refreshTokenEntity.ExpireDate
                });
            }catch (Exception ex)
            {
                return Conflict(new {message = ex.Message, stackTrace = ex.StackTrace});
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest req)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var response = await _authService.RefreshTokenAsync(req.RefreshToken, ipAddress ?? "unknown");
                return Ok(response);
            }catch(Exception ex)
            {
                return Conflict(new {message = ex.Message});
            }
        }
        
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto req)
        {
            try
            {
                var user = await _users.GetUserAsync(req.Email.Trim().ToLower());
                if(user == null){
                    return NotFound(new {message = "User not found"});
                }
                if(!BCrypt.Net.BCrypt.Verify(req.OldPassword, user.PasswordHash))
                   return Unauthorized(new {message = "Invalid old password"});
                await _users.ChangePasswordAsync(user.Id, req.NewPassword);
                return Ok(new {message = "Password changed successfully"});
            }catch(Exception ex)
            {
                return Conflict(new {message = ex.Message});
            }
        }
    }
}