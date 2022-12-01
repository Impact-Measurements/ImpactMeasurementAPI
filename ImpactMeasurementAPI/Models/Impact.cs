using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class Impact
    {
        [Key] [Required] public int Id;

        public double ImpactForce { get; set; }

        public double ImpactDirectionX { get; set; }

        public double ImpactDirectionY { get; set; }

        public double ImpactDirectionZ { get; set; }
    }
}