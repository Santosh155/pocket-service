using pocket_service.DTOs.Auth;
using pocket_service.Models;
using pocket_service.Services.Interfaces;
using pocket_service.Services.InMemory;

namespace pocket_service.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly InMemoryUserStore _store;
        private readonly IConfiguration _config;

        public AuthService(IUserService userService, ITokenService tokenService, InMemoryUserStore store, IConfiguration config)
        {
            _userService = userService;
            _tokenService = tokenService;
            _store = store;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, string ipAddress)
        {
            var exists = await _userService.GetUserAsync(request.Email);
            if(exists != null) throw new InvalidOperationException("User with this email already exists.");

            var user = new User
            {
                Email = request.Email,
                Role = string.IsNullOrWhiteSpace(request.Role) 
                    ? UserRole.User 
                    : Enum.Parse<UserRole>(request.Role, ignoreCase: true),
                EmailVerified = false
            };

            await _userService.CreateAsync(user, request.Password);
            var access = _tokenService.GenerateAccessToken(user);
            var refresh = _tokenService.GenerateRefreshToken();

            var rt = new RefreshToken
            {
                Token = refresh,
                UserId = user.Id,
                ExpireDate = DateTime.UtcNow.AddDays(double.Parse(_config["jwt:RefreshTokenLifetimeDays"]!))
            };
            _store.RefreshTokens[refresh] = rt;
            return new AuthResponse
            {
                Token = refresh,
                ExpireDate = DateTime.UtcNow.AddMinutes(double.Parse(_config["jwt:AccessTokenLifetimeMinutes"]!))
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress)
        {
            var user = await _userService.GetUserAsync(request.Email)
            ?? throw new InvalidOperationException("Invalid Credentials");

            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) 
            throw new InvalidOperationException("Invalid Credentials");

            var access = _tokenService.GenerateAccessToken(user);
            var refresh = _tokenService.GenerateRefreshToken();
            var rt = new RefreshToken
            {
                Token = refresh,
                ExpireDate = DateTime.UtcNow.AddDays(double.Parse(_config["jwt:RefreshTokenLifetimeDays"]!)),
                RemoteIp = ipAddress
            };
            _store.RefreshTokens[refresh] = rt;

            return new AuthResponse
            {
                Token = refresh,
                ExpireDate = DateTime.UtcNow.AddMinutes(double.Parse(_config["jwt:AccessTokenLifetimeMinutes"]!))
            };
        }
        public Task<AuthResponse> RefreshTokenAsync(string token, string ipAddress)
        {
            if(!_store.RefreshTokens.TryGetValue(token, out var rt) || rt.RevokeAt!= null ||
                rt.ExpireDate <= DateTime.UtcNow)
                throw new InvalidOperationException("Invalid Refresh Tokens");

            var user = _store.Users[rt.UserId];
            //rotate
            rt.RevokeAt = DateTime.UtcNow;
            var newRefresh = _tokenService.GenerateRefreshToken();
            var newRt = new RefreshToken
            {
                Token = newRefresh,
                UserId = user.Id,
                ExpireDate = DateTime.UtcNow.AddDays(double.Parse(_config["jwt:RefreshTokenLifetimeDays"]!)),
                RemoteIp = ipAddress
            };
            _store.RefreshTokens[newRefresh] = newRt;
            var access = _tokenService.GenerateAccessToken(user);

            return Task.FromResult(new AuthResponse
            {
                Token = access,
                RefreshToken = newRefresh,
                ExpireDate = DateTime.UtcNow.AddMinutes(double.Parse(_config["jwt:AccessTokenLifetimeMinutes"]!))
            });
        }

        public Task RevokeTokenAsync(string token, string ipAddress)
        {
            if(_store.RefreshTokens.TryGetValue(token, out var rt))
            {
                rt.RevokeAt = DateTime.UtcNow;
            }
            return Task.CompletedTask;
        }

    }
}