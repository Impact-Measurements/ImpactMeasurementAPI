using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    //TODO
    public class Athlete
    {
        [Key] [Required] public int Id { get; set; }
        
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
        
        public string Name { get; set; }
        
        public double Mass { get; set; }
        
        public double MinimumImpactThreshold { get; set; }
        public double MediumImpactThreshold { get; set; }
        public double HighImpactThreshold { get; set; }
    }
}