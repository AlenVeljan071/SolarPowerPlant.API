using SolarPowerPlant.Core.Responses;
using SolarPowerPlant.Core.Responses.TimeSeries;
using SolarPowerPlant.Core.Specifications.TimeSeriesSpecification;

namespace SolarPowerPlant.Core.Interfaces
{
    public interface ITimeSeriesService
    {
        Task<TimeSeriesListResponse> GetTimeSeriesList(TimeSeriesSpecParams request);
       // Task<TimeSeriesListResponse> GetTimeSeriesListPerHour(TimeSeriesSpecParams request);
        Task<CreateResponse> CreateRealProduction(int id, int power);
        Task<CreateResponse> CreateForecastProduction(int id, int power);
    }
}
