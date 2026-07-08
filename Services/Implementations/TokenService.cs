using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pocket_service.Data;
using pocket_service.Models;
using pocket_service.Services.Interfaces;

namespace pocket_service.Services.Implementations
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;
        public TokenService(IConfiguration config, ApplicationDbContext db)
        {
            _config = config;
            _db = db;
        }
        public string GenerateAccessToken(User user)
        {
            var jwtKey = _config["jwt:key"] ?? throw new Exception("JWT key is missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            claims.Add(new Claim (ClaimTypes.Role, user.Role.ToString() ?? "User"));
            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims, 
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["jwt:AccessTokenLifetimeMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}