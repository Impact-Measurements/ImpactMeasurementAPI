using System;
using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class MomentarilyAcceleration
    {
        
        [Key]
        [Required]
        public int Id;
        
        public TrainingSession TrainingSession;

        [Required]
        public int TrainingSessionId;
        
        [Required]
        public int Frame;

        [Required]
        public double AccelerationX;
        
        [Required]
        public double AccelerationY;
        
        [Required]
        public double AccelerationZ;
        
    }
}