using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class Impact
    {
        [Key]
        [Required]
        public int Id;

        public double ImpactForce;

        public double ImpactDirectionX;
        
        public double ImpactDirectionY;
        
        public double ImpactDirectionZ;
    }
}