namespace ImpactMeasurementAPI.DTOs
{
    public class CreateUser
    {
        public string Name { get; set; }
        
        public double Mass { get; set; }
        
        public double MediumImpactThreshold { get; set; }
        
        public double HighImpactThreshold { get; set; }
    }
}