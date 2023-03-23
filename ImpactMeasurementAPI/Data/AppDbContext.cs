using ImpactMeasurementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ImpactMeasurementAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> //DbContext //IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
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
        
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<MomentarilyAcceleration> MomentarilyAccelerations { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Impact> Impacts { get; set; }

        
    }
}