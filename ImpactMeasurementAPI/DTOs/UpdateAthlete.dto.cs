namespace ImpactMeasurementAPI.DTOs
{
    public class UpdateAthlete
    {
        public string Name { get; set; }
        
        public double Mass { get; set; }
        
        public double MediumImpactThreshold { get; set; }
        
        public double HighImpactThreshold { get; set; }
    }
}