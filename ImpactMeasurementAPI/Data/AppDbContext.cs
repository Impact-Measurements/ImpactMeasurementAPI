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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }


    }
}