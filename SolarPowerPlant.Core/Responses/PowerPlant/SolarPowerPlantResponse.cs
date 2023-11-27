using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Responses.TimeSeries;

namespace SolarPowerPlant.Core.Responses.PowerPlant
{
    public class SolarPowerPlantResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int InstalledPower { get; set; }
        public  DateTime InstallationDate { get; set; }
        public  AddressEntity Address { get; set; }
        public  decimal Latitude { get; set; }
        public  decimal Longitude { get; set; }

        public List<TimeSeriesResponse>? TimeSeriesProductions { get; set; }
    }
}
