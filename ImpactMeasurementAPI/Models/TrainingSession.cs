using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class TrainingSession
    {
        
        [Key]
        [Required]
        public int Id;

        public List<MomentarilyAcceleration> FreeAcceleration;

        public DateTime StartingTime;

        // public Sport Sport;
        //
        // public Coach Coach;
    }
}