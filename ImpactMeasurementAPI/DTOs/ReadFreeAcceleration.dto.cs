using System.ComponentModel.DataAnnotations;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.DTOs
{
    public class ReadFreeAcceleration
    {        
        public int Id { get; set; }
        
        public TrainingSession TrainingSession { get; set; }

        public int TrainingSessionId { get; set; }
        
        public int Frame { get; set; }
        
        public double AccelerationX { get; set; }
        
        public double AccelerationY { get; set; }
        
        public double AccelerationZ { get; set; }
    }
}