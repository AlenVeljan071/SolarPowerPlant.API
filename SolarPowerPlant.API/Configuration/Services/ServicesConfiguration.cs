using SolarPowerPlant.API.Services;

namespace SolarPowerPlant.Core.Configuration.Services
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<AccountService>();
            services.AddScoped<SolarPowerPlantService>();
            services.AddScoped<TimeSeriesDataService>();
        }
    }

}
