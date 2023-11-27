using SolarPowerPlant.Core.Enums;

namespace SolarPowerPlant.Core.Entities
{
    public class TimeseriesProduction : BaseEntity, ITrackable
    {
        public  int SolarPowerPlantId { get; set; }
        public  SolarPowerPlant? SolarPowerPlant { get; set; }
        public TimeSerieType TimeSerieType { get; set; }
        public DateTime ProductionTime { get; set; }
        public decimal Production { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
