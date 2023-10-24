using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using td.Shared.Configs;

namespace td.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        }

    }
}
