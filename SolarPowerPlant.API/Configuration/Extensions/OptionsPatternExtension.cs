using SolarPowerPlant.Core.Config;

namespace SolarPowerPlant.API.Configuration.Extensions

{
    public static class OptionsPatternExtensions
    {
        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TokenSettings>(config.GetSection("Token"));
            services.Configure<AesSettings>(config.GetSection("AES"));

            return services;
        }
    }
}
