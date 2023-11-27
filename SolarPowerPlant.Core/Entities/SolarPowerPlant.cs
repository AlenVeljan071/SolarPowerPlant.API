using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPowerPlant.Core.Entities
{
    public class SolarPowerPlant : BaseEntity, ITrackable
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Name{ get; set; }
        public int InstalledPower{ get; set; }
        public required DateTime InstallationDate { get; set; }
        public required AddressEntity Address { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<TimeseriesProduction>? TimeseriesProductions { get; set; }
    }
}
