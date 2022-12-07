namespace ImpactMeasurementAPI.DTOs
{
    public class UpdateUser
    {
        public string Name { get; set; }
        
        public double Mass { get; set; }
        
        public double MediumImpactThreshold { get; set; }
        
        public double HighImpactThreshold { get; set; }
    }
}