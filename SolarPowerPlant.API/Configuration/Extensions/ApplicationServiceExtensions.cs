using SolarPowerPlant.API.Services;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Interfaces.Repositories;
using SolarPowerPlant.Infrastructure.Data;
using SolarPowerPlant.Infrastructure.Data.Repository;
using SolarPowerPlant.Infrastructure.Services;

namespace SolarPowerPlant.API.Configuration.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IReadGenericRepository<>), typeof(ReadGenericRepository<>));
            services.AddScoped<IDataProtection, DataProtection>(); 
            services.AddScoped<ITimeSeriesService, TimeSeriesDataService>();

            return services;
        }
    }
}

