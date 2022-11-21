using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public double Mass { get; set; }
        public double MediumImpactThreshold { get; set; }
        public double HighImpactThreshold { get; set; }
        
    }
}