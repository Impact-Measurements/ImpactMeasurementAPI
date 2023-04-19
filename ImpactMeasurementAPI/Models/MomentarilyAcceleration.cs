using System.ComponentModel.DataAnnotations;

namespace ImpactMeasurementAPI.Models
{
    public class MomentarilyAcceleration
    {
        [Key] [Required] public int Id { get; set; }

        public TrainingSession TrainingSession { get; set; }

        [Required] public int TrainingSessionId { get; set; }

        [Required] public int Frame { get; set; }

        [Required] public double AccelerationX { get; set; }

        [Required] public double AccelerationY { get; set; }

        [Required] public double AccelerationZ { get; set; }
    }
}