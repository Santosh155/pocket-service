using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pocket_service.Data;
using pocket_service.DTOs.Auth;
using pocket_service.Models;
using pocket_service.Services.Interfaces;
using pocket_service.Services.Implementations;

namespace pocket_service.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthService(
            ApplicationDbContext db, IConfiguration config,  ITokenService tokenService)
        {
            _db = db;
            _config = config;
            _tokenService = tokenService;
        }
        public async Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken token)
        {
            try
            {
                _db.RefreshTokens.Add(token);
                await _db.SaveChangesAsync();
                return token;
            } catch (Exception ex)
            {
                throw new Exception("Error saving refresh token: " + ex.Message, ex);
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, string? ipAddress)
        {
            var existingToken = await _db.RefreshTokens.Include(x => x.User)
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);
            if (existingToken == null || !existingToken.IsActive)
                throw new Exception("Invalid or expired refresh token");    
             if(!existingToken.IsActive)
                throw new Exception("Refresh token expired or revoked");

            var user = existingToken.User ?? throw new Exception("User not found");
            existingToken.RevokeAt = DateTime.UtcNow;

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                ExpireDate =
                    DateTime.UtcNow.AddDays(
                        double.Parse(
                            _config["jwt:RefreshTokenLifetimeDays"]!
                        )
                    ),
                RemoteIp = ipAddress,
                ReplacedByToken = newRefreshToken
            };
            _db.RefreshTokens.Add(refreshTokenEntity);
            await _db.SaveChangesAsync();
            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpireDate =
                    DateTime.UtcNow.AddMinutes(
                        double.Parse(
                            _config["jwt:AccessTokenLifetimeMinutes"]!
                        )
                    )
            };
        }
    }
}