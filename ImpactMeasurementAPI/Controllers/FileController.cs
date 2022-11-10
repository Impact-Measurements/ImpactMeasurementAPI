using System;
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
        [HttpPost("postTrainging", Name ="ProcessTrainingDb")]
        public async Task<IActionResult> PostTrainingFile([FromForm] CsvFile document)
        {
            //Hier de verwerking naar de database 
            try
            {
                DatabaseController dbc = new DatabaseController();
            
                try
                {
                    var records = CsvController.ParseCSVFile(document.File);
                    // return records[0].FreeAcc_X.ToString();
                    try
                    {
                        dbc.SaveTraining(records);
                        // return dbc.InsertTraining().ToString();
                        return Ok($"Processed Training {document.Title} training version:{document.Version} - {document.File.FileName} thanks for submitting!");

                    }
                    catch (Exception e)
                    {
                        return NotFound("2" + e.Message);
                    }
                }
                catch(Exception e)
                {
                    return NotFound("1" + e.Message);
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
