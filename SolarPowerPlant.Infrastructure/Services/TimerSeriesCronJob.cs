using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SolarPowerPlant.Core.Interfaces;

namespace SolarPowerPlant.Infrastructure.Services
{
    public class TimerSeriesCronJob : CronJobService
    {
        private readonly ILogger<TimerSeriesCronJob> _logger;
        private readonly IServiceScopeFactory _serviceProvider;

        public TimerSeriesCronJob(IScheduleConfig<TimerSeriesCronJob> scheduleConfig, ILogger<TimerSeriesCronJob> logger, IServiceScopeFactory serviceProvider)
            : base(scheduleConfig.CronExpression, scheduleConfig.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TimeSeriesCronJob  starts.");
            return base.StartAsync(cancellationToken);
        }

        public async override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.UtcNow:hh:mm:ss} TimeSeriesCronJob is working.");

          
            using var scope = _serviceProvider.CreateScope();
            var timeseriesService = scope.ServiceProvider.GetRequiredService<ITimeSeriesService>();
            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var solarPowerPlants = await _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()!.ListAllAsync();
            foreach (var solarPowerPlant in solarPowerPlants)
            {
                try
                {
                    await timeseriesService.CreateRealProductionAsync(solarPowerPlant.Id, solarPowerPlant.InstalledPower);
                    await timeseriesService.CreateForecastProductionAsync(solarPowerPlant.Id, solarPowerPlant.InstalledPower);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Time series Cron job An exception occurred: {ex.Message}");
                }
            }
          
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("TimeSeriesCronJob  is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
