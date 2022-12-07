using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class TrainingSession
    {
        [Key] [Required] public int Id { get; set; }

        public int UserId { get; set; }
        public ICollection<MomentarilyAcceleration> FreeAcceleration { get; set; }

        public DateTime StartingTime { get; set; }
        
        public int EffectivenessScore { get; set; }
        
        public int PainfulnessScore { get; set; }
        
        public ICollection<Impact> Impacts { get; set; }

        // public Sport Sport;
        //
        // public Coach Coach;
    }
}