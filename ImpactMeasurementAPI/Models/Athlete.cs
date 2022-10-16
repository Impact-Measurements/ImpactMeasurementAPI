using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    
    //TODO
    public class Athlete
    {
        
        [Key]
        [Required]
        public int Id { get; set; }
    }
}