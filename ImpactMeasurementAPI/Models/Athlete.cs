using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    
    //TODO
    public class Athlete
    {
        
        [Key]
        [Required]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        
    }
}