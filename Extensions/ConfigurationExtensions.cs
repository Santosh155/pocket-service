using Microsoft.Extensions.Configuration;

namespace pocket_service.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetJwtKey(this Iconfiguration config)=> 
            config.GetSection("jwt:key").value ?? throw new Exception("JWT key not found");
    }
}