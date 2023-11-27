using SolarPowerPlant.Core.Enums;
using SolarPowerPlant.Core.Specifications.BaseSpecification;

namespace SolarPowerPlant.Core.Specifications.TimeSeriesSpecification
{
    public class TimeSeriesSpecification : BaseSpecification<Entities.TimeseriesProduction>
    {
        public TimeSeriesSpecification(TimeSeriesSpecParams specParams) : base(x =>
         (specParams.SolarPowerPlantId == x.SolarPowerPlantId) &&
         (!specParams.TimeSerieType.HasValue || x.TimeSerieType == (TimeSerieType)specParams.TimeSerieType) &&
         (!specParams.DateFrom.HasValue || specParams.DateFrom! <= x.ProductionTime) &&
         (!specParams.DateTo.HasValue || specParams.DateTo! >= x.ProductionTime) &&
         (!specParams.UserId.HasValue || specParams.UserId == x.SolarPowerPlant!.UserId) 
        )
        {



        }
    }
}
