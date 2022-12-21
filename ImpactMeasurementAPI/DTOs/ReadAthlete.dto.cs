namespace ImpactMeasurementAPI.DTOs
{
    public class ReadAthlete
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public double Mass { get; set; }
        
        public double MinimumImpactThreshold { get; set; }
        public double MediumImpactThreshold { get; set; }
        public double HighImpactThreshold { get; set; }
    }
}