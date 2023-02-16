using Microsoft.AspNetCore.Http;

namespace ImpactMeasurementAPI.Models
{
    public class CsvFile
    {
        // public string Title { get; set; }
        // public string Version { get; set; }
        public IFormFile File { get; set; }
    }
}