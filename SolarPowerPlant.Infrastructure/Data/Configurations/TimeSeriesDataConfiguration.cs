using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolarPowerPlant.Core.Entities;

namespace SolarPowerPlant.Infrastructure.Data.Configurations
{
    public class TimeSeriesDataConfiguration : IEntityTypeConfiguration<TimeseriesProduction>
    {
        public void Configure(EntityTypeBuilder<TimeseriesProduction> builder)
        {

            builder.HasOne(x => x.SolarPowerPlant)
               .WithMany(x => x.TimeseriesProductions)
               .HasForeignKey(x => x.SolarPowerPlantId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Production)
                  .HasPrecision(14, 2);
        }
    
    }
}
