using Microsoft.AspNetCore.Mvc;
using pocket_service.DTOs.Auth;
using pocket_service.Services.Interfaces;

namespace pocket_service.Controller
{
    [ApiController]
    [Route("api/auth")]

    public class AuthController: ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var res = await _auth.RegisterAsync(req, ip ?? "unknown");
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var res = await _auth.LoginAsync(req, ip ?? "Unknown");
            return Ok(res);
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var res = await _auth.RefreshTokenAsync(req, ip ?? "unknown");
            return Ok(res);
        }
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeRequest req)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var res = await _auth.RevokeTokenAsync(req, ip ?? "unknown");
            return NoContext();
        }
    }
}