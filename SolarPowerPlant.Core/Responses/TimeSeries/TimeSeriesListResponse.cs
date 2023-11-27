using SolarPowerPlant.Core.Helpers;

namespace SolarPowerPlant.Core.Responses.TimeSeries
{
    public class TimeSeriesListResponse
    {
        public Pagination<TimeSeriesResponse>? Pagination { get; set; }
    }
}
