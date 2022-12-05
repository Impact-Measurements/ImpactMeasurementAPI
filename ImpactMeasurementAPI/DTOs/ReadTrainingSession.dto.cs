using System;
using System.Collections.Generic;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.DTOs
{
    public class ReadTrainingSession
    {
        public int Id { get; set; }

        public string StartingTime { get; set; }
        
        public int EffectivenessScore { get; set; }
        
        public int PainfulnessScore { get; set; }

        public IEnumerable<ReadImpact> Impacts { get; set; }
    }
}