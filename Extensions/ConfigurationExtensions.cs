using Microsoft.Extensions.Configuration;

namespace pocket_service.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtKey(this IConfiguration config)=> 
            config.GetSection("jwt:key").Value ?? throw new Exception("JWT key not found");
    }
}