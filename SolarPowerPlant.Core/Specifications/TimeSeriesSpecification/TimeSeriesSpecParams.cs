using SolarPowerPlant.Core.Enums;
using SolarPowerPlant.Core.Specifications.BaseSpecification;

namespace SolarPowerPlant.Core.Specifications.TimeSeriesSpecification
{
    public class TimeSeriesSpecParams : BaseMinimalSpecParams
    {
        public int SolarPowerPlantId { get; set; }
        public int? TimeSerieType { get; set; }
        public Granularity Granularity { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
