using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class FreeAcceleration
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        public Axis Axis { get; set; }
        
        public DateTime StartSessionTime { get; set; }
        
        public ICollection<double> Values { get; set; }
    }
}