using System;
using System.Collections.Generic;

namespace ImpactMeasurementAPI.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }
        
        public DateTime StartSessionTime { get; set; }
        
        public FreeAcceleration FreeAccelerationX { get; set; }
        public FreeAcceleration FreeAccelerationY { get; set; }
        public FreeAcceleration FreeAccelerationZ { get; set; }
    }
}