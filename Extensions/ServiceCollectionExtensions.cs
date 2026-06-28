using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pocket_service.Services.Implementations;
using pocket_service.Services.Interfaces;
using pocket_service.Services.InMemory;

namespace pocket_service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerService(this IServiceCollection services, IConfiguration config)
        {
            // Register application services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            // Add to IMemory and replace it with EF/Dbcontext later
            services.AddScoped<InMemoryUserStore>();

            return services;
        }
    }
}