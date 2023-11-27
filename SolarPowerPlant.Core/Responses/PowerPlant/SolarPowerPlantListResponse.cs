using SolarPowerPlant.Core.Helpers;

namespace SolarPowerPlant.Core.Responses.PowerPlant
{
    public class SolarPowerPlantListResponse
    {
        public Pagination<SolarPowerPlantResponse>? Pagination { get; set; }
    }
}
