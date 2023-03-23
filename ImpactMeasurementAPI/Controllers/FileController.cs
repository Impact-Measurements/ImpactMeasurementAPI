using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("postTrainging", Name = "ProcessTrainingDb")]
        public async Task<IActionResult> PostTrainingFile( int UserId, int Effect, int Pain, [FromForm] CsvFile document)
        {
            //Hier de verwerking naar de database 
            try
            {
                var dbc = new DatabaseController();

                try
                {
                    var records = CsvController.ParseCSVFile(document.File);
                    // return records[0].FreeAcc_X.ToString();
                    try
                    {
                        //TODO Unhardcode this
                        //dbc.SaveTraining(records, UserId, Effect, Pain);
                        var id = dbc.SaveTraining(records, 1, Effect, Pain);
                        // return dbc.InsertTraining().ToString();
                        return Ok(
                            //                            $"Processed Training {document.Title} training version:{document.Version} - {document.File.FileName} thanks for submitting!");
                            $"Processed Training thanks for submitting! {records.Count}, {id}");

                    }
                    catch (Exception e)
                    {
                        return NotFound("2" + e.Message);
                    }
                }
                catch (Exception e)
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