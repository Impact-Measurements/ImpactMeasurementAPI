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
        private readonly IFreeAccelerationRepo _freeAccelerationRepository;
        private readonly IAthleteRepo _userRepo;

        public ImpactController(IFreeAccelerationRepo freeAccelerationRepository, IAthleteRepo userRepo, IMapper mapper)
        {
            _freeAccelerationRepository = freeAccelerationRepository;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        
                
        [HttpPost("trainingsession/create", Name = "CreateTrainingSession")]
        public ActionResult<ReadTrainingSession> CreateTrainingSession(CreateTrainingSession createTrainingSession)
        {
            TrainingSession trainingSession = new TrainingSession();
            trainingSession = _mapper.Map<TrainingSession>(createTrainingSession);
            _freeAccelerationRepository.CreateTrainingSession(trainingSession);
            _freeAccelerationRepository.SaveChanges();
            Console.WriteLine(JsonSerializer.Serialize(trainingSession));
            return _mapper.Map<ReadTrainingSession>(trainingSession);
        }

        
        [HttpPut("trainingsession/update", Name = "UpdateTrainingSession")]
        public ActionResult<ReadTrainingSession> UpdateTrainingSession(UpdateTrainingSession updateTrainingSession)
        {
            TrainingSession trainingSession = _freeAccelerationRepository.GetTrainingSession(updateTrainingSession.Id);
            trainingSession.EffectivenessScore = updateTrainingSession.EffectivenessScore;
            trainingSession.PainfulnessScore = updateTrainingSession.PainfulnessScore;
            _freeAccelerationRepository.SaveChanges();
            return _mapper.Map<ReadTrainingSession>(trainingSession);
        }
        
        [HttpGet("trainingsession/{trainingSessionId}", Name = "GetTrainingSession")]
        public ActionResult<TrainingSession> GetTrainingSession(int trainingSessionId)
        {
            var trainingSession = _freeAccelerationRepository.GetTrainingSession(trainingSessionId);
            if (trainingSession == null)
            {
                return NotFound();
            }

            var readTraining = _mapper.Map<ReadTrainingSession>(trainingSession);
            readTraining.Impacts =
                _mapper.Map<IEnumerable<ReadImpact>>(_freeAccelerationRepository.GetAllImpactDataFromSession(trainingSessionId));

            readTraining.StartingTime = trainingSession.StartingTime.ToString("d");
            
            return Ok(readTraining);

        }
        
        [HttpGet("trainingsession/all/{userId}", Name = "GetAllTrainingSessionsWithUserId")]
        public ActionResult<IEnumerable<ReadTrainingSession>> GetTrainingSessionsWithUserId(int userId)
        {
            var userItem = _freeAccelerationRepository.GetAllTrainingSessions(userId).ToList();
            var readTrainingSession = _mapper.Map<IEnumerable<ReadTrainingSession>>(userItem).ToList();

            for (int i = 0; i < userItem.Count; i++)
            {
                readTrainingSession[i].StartingTime = userItem[i].StartingTime.ToString("d");
            }
            return Ok(readTrainingSession);
        }
        
       
        [HttpGet("trainingsession/all", Name = "GetAllTrainingSessions")]
        public ActionResult<IEnumerable<ReadTrainingSession>> GetTrainingSessions()
        {
            var userItem = _freeAccelerationRepository.GetAllTrainingSessions().ToList();
            var readTrainingSession = _mapper.Map<IEnumerable<ReadTrainingSession>>(userItem).ToList();

            for (int i = 0; i < userItem.Count; i++)
            {
                readTrainingSession[i].StartingTime = userItem[i].StartingTime.ToString("d");
            }
            return Ok(readTrainingSession);
        }


        [HttpGet("impact/average/{trainingSessionId}", Name = "GetAverageImpact")]
        public ActionResult<double> GetAverageImpact(int trainingSessionId)
        {
            if (!TrainingSessionExists(trainingSessionId))
            {
                return NotFound();
            }
            
            double averageImpact = _freeAccelerationRepository.GetAverageForceOfImpactFromSession(trainingSessionId);
            
            return Ok(averageImpact);

        }
        
        [HttpGet("impact/all/{trainingSessionId}", Name = "GetAllImpact")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllImpact(int trainingSessionId)
        {
            var allImpact = _freeAccelerationRepository.GetAllImpactDataFromSession(trainingSessionId);

            if (allImpact != null && allImpact.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));
            }

            return NotFound();
        }
        
        [HttpGet("impact/all/with_threshold/{trainingSessionId}", Name = "GetAllImpactWithThreshold")]
        public ActionResult<ReadTrainingSession> GetAllImpactWithThreshold(int trainingSessionId)
        {

            var trainingsession = _freeAccelerationRepository.GetTrainingSession(trainingSessionId);
            if (trainingsession.UserId == 0)
            {
                return NotFound();
            }
            var user = _userRepo.GetUserById(trainingsession.UserId);
            var readTrainingSession = _mapper.Map<ReadTrainingSession>(trainingsession);
            
            var allImpact = _freeAccelerationRepository.GetAllImpactDataFromSession(trainingSessionId, user.MinimumImpactThreshold);

            readTrainingSession.Impacts = _mapper.Map<IEnumerable<ReadImpact>>(allImpact);

            if (allImpact != null && allImpact.Count() != 0)
            {
                return Ok(readTrainingSession);
            }

            return NotFound();
        }
        
        // [HttpGet("impact/all/with_threshold/{trainingSessionId}", Name = "GetAllImpactWithThreshold")]
        // public ActionResult<IEnumerable<ReadImpact>> GetAllImpactWithThreshold(int trainingSessionId)
        // {
        //
        //     var trainingsession = _freeAccelerationRepository.GetTrainingSession(trainingSessionId);
        //     if (trainingsession.UserId == 0)
        //     {
        //         return NotFound();
        //     }
        //     var user = _userRepo.GetUserById(trainingsession.UserId);
        //     
        //     var allImpact = _freeAccelerationRepository.GetAllImpactDataFromSession(trainingSessionId, user.MinimumImpactThreshold);
        //
        //     if (allImpact != null && allImpact.Count() != 0)
        //     {
        //         return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));
        //     }
        //
        //     return NotFound();
        // }
        
        [HttpGet("impact/low/with_threshold/{trainingSessionId}", Name = "GetAllImpactWithLowThreshold")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllLowImpact(int trainingSessionId)
        {
            var allImpact = _freeAccelerationRepository.GetAllImpactDataFromImpactZone(trainingSessionId, "low");

            if (allImpact != null && allImpact.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));
            }

            return NotFound();
        }
        
        [HttpGet("impact/medium/with_threshold/{trainingSessionId}", Name = "GetAllImpactWithMediumThreshold")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllMediumImpact(int trainingSessionId)
        {
            var allImpact = _freeAccelerationRepository.GetAllImpactDataFromImpactZone(trainingSessionId, "medium");

            if (allImpact != null && allImpact.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));
            }

            return NotFound();
        }
        
        [HttpGet("impact/high/with_threshold/{trainingSessionId}", Name = "GetAllImpactWithHighThreshold")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllHighImpact(int trainingSessionId)
        {
            var allImpact = _freeAccelerationRepository.GetAllImpactDataFromImpactZone(trainingSessionId, "high");

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
            
            Impact highestImpact = _freeAccelerationRepository.GetHighestForceOfImpactFromSession(trainingSessionId);
            return Ok(_mapper.Map<ReadImpact>(highestImpact));

        }

        [HttpPost("acceleration/create", Name = "CreateFreeAcceleration")]
        public ActionResult<ReadImpact> CreateFreeAcceleration(List<CreateMomentarilyAcceleration> createMomentarilyAccelerations)
        {
            List<MomentarilyAcceleration> momentarilyAccelerations = new List<MomentarilyAcceleration>();

            foreach (var createMomentarilyAcceleration in createMomentarilyAccelerations)
            {
                MomentarilyAcceleration momentarilyAcceleration =
                    _mapper.Map<MomentarilyAcceleration>(createMomentarilyAcceleration);
                
                _freeAccelerationRepository.CreateMomentarilyAcceleration(momentarilyAcceleration);
                _freeAccelerationRepository.SaveChanges();
                
                momentarilyAccelerations.Add(momentarilyAcceleration);
            }

            _freeAccelerationRepository.SaveChanges();
            
            CalculateImpact calculateImpact = new CalculateImpact(momentarilyAccelerations, 74);
            Impact highestImpact = calculateImpact.CalculateAllImpacts().FirstOrDefault();
            var readImpact = _mapper.Map<ReadImpact>(highestImpact);
            return readImpact;
        }
        
        [HttpGet("acceleration/all/{trainingSessionId}", Name = "GetAllFreeAcceleration")]
        public ActionResult<IEnumerable<ReadFreeAcceleration>> GetFreeAcceleration(int trainingSessionId)
        {
            var freeAcceleration = _freeAccelerationRepository.GetAllFreeAccelerationValuesFromSession(trainingSessionId);

            if (freeAcceleration != null && freeAcceleration.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ReadFreeAcceleration>>(freeAcceleration));
            }

            return NotFound();
        }


        private bool TrainingSessionExists(int id)
        {
            if (_freeAccelerationRepository.GetTrainingSession(id) != null)
            {
                return true;
            }

            return false;
        }


    }
}