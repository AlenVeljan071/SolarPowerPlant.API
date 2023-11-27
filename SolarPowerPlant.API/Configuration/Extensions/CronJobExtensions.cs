using SolarPowerPlant.Infrastructure.Services;

namespace SolarPowerPlant.API.Extensions
{
    public static class CronJobExtensions
    {
        public static IServiceCollection AddCronJobService(this IServiceCollection services)
        {
            

            services.AddHostedService<TimerSeriesCronJob>();
            services.AddCronJob<TimerSeriesCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Utc;
                c.CronExpression = @"0,15,30,45 * * * *";
               // c.CronExpression = @"*/1 * * * *";
            });
           

            return services;
        }
    }
}
