using SolarPowerPlant.Core.Enums;

namespace SolarPowerPlant.Core.Responses.TimeSeries
{
    public class TimeSeriesResponse
    {
        public int Id { get; set; }
        public int SolarPowerPlantId { get; set; }
        public EnumValue? Granularity { get; set; }
        public EnumValue? TimeSerieType { get; set; }
        public DateTime ProductionTime { get; set; }
        public decimal Production { get; set; }
    }
}
