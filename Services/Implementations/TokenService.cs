using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using pocket_service.Models;
using pocket_service.Services.Interfaces;

namespace pocket_service.Services.Implementations
{
    public class TokenService: ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) => _config = config;
        public string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetJwtKey()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));
            var token = new JwtSecurityToken(
                isuser: _config["jwt: Isuser"],
                audience: _config["jwt: Audience"],
                claims: claims, 
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["jwt: AccessTokensLifetimeinutes"])),
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