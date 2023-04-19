using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class Impact
    {
        [Key]
        [Required]
        public int Id;
        
        public TrainingSession TrainingSession { get; set; }

        [Required]
        public int TrainingSessionId { get; set; }
        
        public int Frame { get; set; }

        public double ImpactForce { get; set; }

        public double ImpactDirectionX { get; set; }

        public double ImpactDirectionY { get; set; }

        public double ImpactDirectionZ { get; set; }
        
        public Color Color { get; set; }
    }
}