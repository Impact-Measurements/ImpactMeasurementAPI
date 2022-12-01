namespace ImpactMeasurementAPI.DTOs
{
    public class CreateMomentarilyAcceleration
    {
        public int TrainingSessionId { get; set; }

        public int Frame { get; set; }

        public double AccelerationX { get; set; }

        public double AccelerationY { get; set; }

        public double AccelerationZ { get; set; }
    }
}