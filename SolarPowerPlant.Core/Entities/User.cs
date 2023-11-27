using SolarPowerPlant.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarPowerPlant.Core.Entities
{
    public class User : BaseEntity, ITrackable
    {
        [Column(TypeName = "varchar(100)")]
        public  string? FirstName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string?  LastName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Email { get; set; }
        public AddressEntity? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? PhoneNumber { get; set; }
       
        [Column(TypeName = "varchar(100)")]
        public string? Password { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Salt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public List<SolarPowerPlant>? SolarPowerPlants { get; set; }
    }
}
