using ImpactMeasurementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpactMeasurementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<MomentarilyAcceleration> MomentarilyAccelerations { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<AthleteOld> AthletesOld { get; set; }
        public DbSet<Impact> Impacts { get; set; }
        public DbSet<Athlete> Athletes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TrainingSession>()
                .HasMany(t => t.FreeAcceleration)
                .WithOne(m => m.TrainingSession!)
                .HasForeignKey(t => t.Id);

            modelBuilder
                .Entity<MomentarilyAcceleration>()
                .HasOne(m => m.TrainingSession)
                .WithMany(t => t.FreeAcceleration)
                .HasForeignKey(m => m.TrainingSessionId);

            modelBuilder
                .Entity<TrainingSession>()
                .HasMany(t => t.Impacts)
                .WithOne(m => m.TrainingSession!)
                .HasForeignKey(t => t.Id);

            modelBuilder
                .Entity<Impact>()
                .HasOne(m => m.TrainingSession)
                .WithMany(t => t.Impacts)
                .HasForeignKey(m => m.TrainingSessionId);
        }


    }
}