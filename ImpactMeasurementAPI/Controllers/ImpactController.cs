using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("CorsPolicy")]
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

            var readTraining = _mapper.Map<ReadTrainingSession>(trainingSession);
            readTraining.Impacts =
                _mapper.Map<IEnumerable<ReadImpact>>(_repository.GetAllImpactDataFromSession(trainingSessionId));
            
            if (trainingSession != null)
            {
                return Ok(readTraining);
            }
            
            return NotFound();
        }
        
        // //TODO change to post once csv upload works
        // [HttpGet("trainingsession/save", Name = "SaveTrainingSession")]
        // public ActionResult<string> SaveTrainingSession()
        // {
        //     DatabaseController dbc = new DatabaseController();
        //     
        //     try
        //     {
        //         var records = CsvController.ParseCSV();
        //         // return records[0].FreeAcc_X.ToString();
        //         try
        //         {
        //             dbc.SaveTraining(records);
        //             // return dbc.InsertTraining().ToString();
        //             return Ok();
        //         }
        //         catch (Exception e)
        //         {
        //             return NotFound("2" + e.Message);
        //         }
        //     }
        //     catch(Exception e)
        //     {
        //         return NotFound("1" + e.Message);
        //     }
        //     
        // }

        [HttpGet("acceleration/all/{trainingSessionId}", Name = "GetAllFreeAcceleration")]
        public ActionResult<IEnumerable<ReadFreeAcceleration>> GetFreeAcceleration(int trainingSessionId)
        {
            var freeAcceleration = _repository.GetAllFreeAccelerationValuesFromSession(trainingSessionId);

            if (freeAcceleration != null && freeAcceleration.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadFreeAcceleration>>(freeAcceleration));
            }

            return NotFound();
        }
        
        [HttpGet("impact/average/{trainingSessionId}", Name = "GetAverageImpact")]
        public ActionResult<double> GetAverageImpact(int trainingSessionId)
        {
            if (!TrainingSessionExists(trainingSessionId))
            {
                return NotFound();
            }
            
            double averageImpact = _repository.GetAverageForceOfImpactFromSession(trainingSessionId);
            
            return Ok(averageImpact);

        }
        
        [HttpGet("impact/all/{trainingSessionId}", Name = "GetAllImpact")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllImpact(int trainingSessionId)
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
            
            Impact highestImpact = _repository.GetHighestForceOfImpactFromSession(trainingSessionId);
            return Ok(_mapper.Map<ReadImpact>(highestImpact));

        }

        [HttpPost("training/create", Name = "CreateTrainingSession")]
        public ActionResult<ReadTrainingSession> CreateTrainingSession(CreateTrainingSession createTrainingSession)
        {
            TrainingSession trainingSession = new TrainingSession();
            trainingSession = _mapper.Map<TrainingSession>(createTrainingSession);
            _repository.CreateTrainingSession(trainingSession);
            _repository.SaveChanges();
            Console.WriteLine(JsonSerializer.Serialize(trainingSession));
            return _mapper.Map<ReadTrainingSession>(trainingSession);
        }
        
        [HttpPost("training/update", Name = "UpdateTrainingSession")]
        public ActionResult<ReadTrainingSession> CreateTrainingSession(UpdateTrainingSession updateTrainingSession)
        {
            TrainingSession trainingSession = _repository.GetTrainingSession(updateTrainingSession.Id);
            trainingSession.EffectivenessScore = updateTrainingSession.EffectivenessScore;
            trainingSession.PainfulnessScore = updateTrainingSession.PainfulnessScore;
            _repository.SaveChanges();
            return _mapper.Map<ReadTrainingSession>(trainingSession);
        }
        
        [HttpPost("acceleration/create", Name = "CreateFreeAcceleration")]
        public ActionResult<ReadImpact> CreateFreeAcceleration(List<CreateMomentarilyAcceleration> createMomentarilyAccelerations)
        {
            List<MomentarilyAcceleration> momentarilyAccelerations = new List<MomentarilyAcceleration>();

            foreach (var createMomentarilyAcceleration in createMomentarilyAccelerations)
            {
                MomentarilyAcceleration momentarilyAcceleration =
                    _mapper.Map<MomentarilyAcceleration>(createMomentarilyAcceleration);
                
                _repository.CreateMomentarilyAcceleration(momentarilyAcceleration);
                _repository.SaveChanges();
                
                momentarilyAccelerations.Add(momentarilyAcceleration);
            }

            _repository.SaveChanges();
            
            CalculateImpact calculateImpact = new CalculateImpact(momentarilyAccelerations, 74);
            Impact highestImpact = calculateImpact.CalculateAllImpacts().FirstOrDefault();
            var readImpact = _mapper.Map<ReadImpact>(highestImpact);
            return readImpact;
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