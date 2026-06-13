using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pocket_service.Services.Implementations;
using pocket_service.Services.Interfaces;
using pocket_service.Services.InMemory;

namespace pocket_service.Extensions
{
    public class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerService(this IServiceCollection Services, IConfiguration config)
        {
            // Register application services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ITokenService, TokenService>();

            // Add to IMemory and replace it with EF/Dbcontext later
            services.AddSingleton<InMemoryUserStore>();

            return services;
        }
    }
}