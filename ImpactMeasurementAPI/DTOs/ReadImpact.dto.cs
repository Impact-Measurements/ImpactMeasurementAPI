namespace ImpactMeasurementAPI.DTOs
{
    public class ReadImpact
    {
        public int Id { get; set; }
        
        public int Frame { get; set; }

        public double ImpactForce { get; set; }

        public double ImpactDirectionX { get; set; }

        public double ImpactDirectionY { get; set; }

        public double ImpactDirectionZ { get; set; }
    }
}