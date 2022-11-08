using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("training", Name ="ProcessTrainingDb")]
        public async Task<IActionResult> PostTrainingFile([FromForm] CsvFile document)
        {
            //Hier de verwerking naar de database 

            return Ok($"Processed Training {document.Title} training version:{document.Version} - {document.File.FileName} thanks for submitting!");
        }
    }
}
