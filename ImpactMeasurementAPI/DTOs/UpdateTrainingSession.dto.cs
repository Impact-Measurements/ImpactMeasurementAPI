namespace ImpactMeasurementAPI.DTOs
{
    public class UpdateTrainingSession
    {
        public int Id { get; set; }
        
        public int EffectivenessScore { get; set; }
        
        public int PainfulnessScore { get; set; }
    }
}