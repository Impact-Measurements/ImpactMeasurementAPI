using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class TrainingSession
    {
        
        [Key]
        [Required]
        public int Id { get; set; }

        public List<MomentarilyAcceleration> FreeAcceleration { get; set; }

        [Required]
        public DateTime StartingTime { get; set; }

        // public Sport Sport;
        //
        // public Coach Coach;
    }
}