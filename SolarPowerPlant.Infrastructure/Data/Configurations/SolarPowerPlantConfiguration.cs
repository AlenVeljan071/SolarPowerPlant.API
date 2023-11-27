using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SolarPowerPlant.Infrastructure.Data.Configurations
{
    public class SolarPowerPlantConfiguration : IEntityTypeConfiguration<Core.Entities.SolarPowerPlant>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.SolarPowerPlant> builder)
        {

            builder.HasOne(x => x.User)
             .WithMany(x => x.SolarPowerPlants)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Latitude)
                .HasPrecision(12, 9);

            builder.Property(x => x.Longitude)
            .HasPrecision(12, 9);

            builder.OwnsOne(x => x.Address);
             
        }
    }

}
