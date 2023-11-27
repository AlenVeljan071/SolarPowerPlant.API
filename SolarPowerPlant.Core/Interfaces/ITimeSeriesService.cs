using SolarPowerPlant.Core.Responses;
using SolarPowerPlant.Core.Responses.TimeSeries;
using SolarPowerPlant.Core.Specifications.TimeSeriesSpecification;

namespace SolarPowerPlant.Core.Interfaces
{
    public interface ITimeSeriesService
    {
        Task<TimeSeriesListResponse> GetTimeSeriesListAsync(TimeSeriesSpecParams request);
        Task<CreateResponse> CreateRealProductionAsync(int id, int power);
        Task<CreateResponse> CreateForecastProductionAsync(int id, int power);
    }
}
