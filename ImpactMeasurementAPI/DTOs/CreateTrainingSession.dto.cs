using System;

namespace ImpactMeasurementAPI.DTOs
{
    public class CreateTrainingSession
    {
        public DateTime StartingTime { get; set; }
        
        public int EffectivenessScore { get; set; }
        
        public int PainfulnessScore { get; set; }
    }
}