using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ImpactController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFreeAccelerationRepo _repository;
        
        public ImpactController(IFreeAccelerationRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("trainingsession/{trainingSessionId}", Name = "GetTrainingSession")]
        public ActionResult<TrainingSession> GetTrainingSession(int trainingSessionId)
        {
            var trainingSession = _repository.GetTrainingSession(trainingSessionId);
            if (trainingSession != null)
            {
                return Ok(_mapper.Map<ReadTrainingSession>(trainingSession));
            }

            return NotFound();
        }
        
        //TODO change to post once csv upload works
        [HttpGet("trainingsession/save", Name = "SaveTrainingSession")]
        public ActionResult<string> SaveTrainingSession()
        {
            DatabaseController dbc = new DatabaseController();
            
            try
            {
                var records = CsvController.ParseCSV();
                // return records[0].FreeAcc_X.ToString();
                try
                {
                    dbc.SaveTraining(records);
                    // return dbc.InsertTraining().ToString();
                    return Ok();
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

        [HttpGet("acceleration/all/{trainingSessionId}", Name = "GetAllFreeAcceleration")]
        public ActionResult<TrainingSession> GetFreeAcceleration(int trainingSessionId)
        {
            var freeAcceleration = _repository.GetAllFreeAccelerationValuesFromSession(trainingSessionId);

            if (freeAcceleration != null && freeAcceleration.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadFreeAcceleration>>(freeAcceleration));
            }

            return NotFound();
        }
        
        [HttpGet("acceleration/average/{trainingSessionId}", Name = "GetAverageImpact")]
        public ActionResult<double> GetAverageImpact(int trainingSessionId)
        {
            if (!TrainingSessionExists(trainingSessionId))
            {
                return NotFound();
            }
            
            var averageImpact = _repository.GetAverageForceOfImpactFromSession(trainingSessionId);
            
            return Ok(_mapper.Map<ReadImpact>(averageImpact));

        }
        
        [HttpGet("impact/all/{trainingSessionId}", Name = "GetAllImpact")]
        public ActionResult<Impact> GetAllImpact(int trainingSessionId)
        {
            var allImpact = _repository.GetAllImpactDataFromSession(trainingSessionId);

            if (allImpact != null && allImpact.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));
            }

            return NotFound();

        }
        
        [HttpGet("impact/highest/{trainingSessionId}", Name = "GetHighestImpact")]
        public ActionResult<double> GetHighestImpact(int trainingSessionId)
        {

            if (!TrainingSessionExists(trainingSessionId))
            {
                return NotFound();
            }
            
            var highestImpact = _repository.GetHighestForceOfImpactFromSession(trainingSessionId);

            return Ok(_mapper.Map<ReadImpact>(highestImpact));

        }

        private bool TrainingSessionExists(int id)
        {
            if (_repository.GetTrainingSession(id) != null)
            {
                return true;
            }

            return false;
        }


    }
}