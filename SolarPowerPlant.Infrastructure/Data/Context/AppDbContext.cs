using Microsoft.EntityFrameworkCore;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Infrastructure.Data.Configurations;

namespace SolarPowerPlant.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
      

        // USER
        public DbSet<User> User { get; set; }

        //SOLAR POWER PLANT
        public DbSet<Core.Entities.SolarPowerPlant> SolarPowerPlant { get; set; }
        public DbSet<TimeseriesProduction> TimeseriesProduction { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new SolarPowerPlantConfiguration());
        }

        public override int SaveChanges()
        {
            UpdateDateTime();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            UpdateDateTime();
            return base.SaveChangesAsync(cancellationToken);
        }
        private void UpdateDateTime()
        {
            var entries = ChangeTracker
                 .Entries()
                 .Where(e => e.Entity is ITrackable && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((ITrackable)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((ITrackable)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

        }
    }
}
