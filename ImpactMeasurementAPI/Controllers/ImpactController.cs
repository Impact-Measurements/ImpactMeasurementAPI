using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
            if (trainingSession != null) return Ok(_mapper.Map<ReadTrainingSession>(trainingSession));

            return NotFound();
        }

        [HttpGet("acceleration/all/{trainingSessionId}", Name = "GetAllFreeAcceleration")]
        public ActionResult<IEnumerable<ReadFreeAcceleration>> GetFreeAcceleration(int trainingSessionId)
        {
            var freeAcceleration = _repository.GetAllFreeAccelerationValuesFromSession(trainingSessionId);

            if (freeAcceleration != null && freeAcceleration.Count() != 0)
                return Ok(_mapper.Map<IEnumerable<ReadFreeAcceleration>>(freeAcceleration));

            return NotFound();
        }

        [HttpGet("impact/average/{trainingSessionId}", Name = "GetAverageImpact")]
        public ActionResult<double> GetAverageImpact(int trainingSessionId)
        {
            if (!TrainingSessionExists(trainingSessionId)) return NotFound();

            var averageImpact = _repository.GetAverageForceOfImpactFromSession(trainingSessionId);

            return Ok(averageImpact);
        }

        [HttpGet("impact/all/{trainingSessionId}", Name = "GetAllImpact")]
        public ActionResult<IEnumerable<ReadImpact>> GetAllImpact(int trainingSessionId)
        {
            var allImpact = _repository.GetAllImpactDataFromSession(trainingSessionId);

            if (allImpact != null && allImpact.Count() != 0) return Ok(_mapper.Map<IEnumerable<ReadImpact>>(allImpact));

            return NotFound();
        }

        [HttpGet("impact/highest/{trainingSessionId}", Name = "GetHighestImpact")]
        public ActionResult<double> GetHighestImpact(int trainingSessionId)
        {
            if (!TrainingSessionExists(trainingSessionId)) return NotFound();

            var highestImpact = _repository.GetHighestForceOfImpactFromSession(trainingSessionId);
            return Ok(_mapper.Map<ReadImpact>(highestImpact));
        }

        [HttpPost("training/create", Name = "CreateTrainingSession")]
        public ActionResult<ReadTrainingSession> CreateTrainingSession(CreateTrainingSession createTrainingSession)
        {
            var trainingSession = new TrainingSession();
            trainingSession = _mapper.Map<TrainingSession>(createTrainingSession);
            _repository.CreateTrainingSession(trainingSession);
            _repository.SaveChanges();
            Console.WriteLine(JsonSerializer.Serialize(trainingSession));
            return _mapper.Map<ReadTrainingSession>(trainingSession);
        }

        [HttpPost("acceleration/create", Name = "CreateFreeAcceleration")]
        public ActionResult<ReadImpact> CreateFreeAcceleration(
            List<CreateMomentarilyAcceleration> createMomentarilyAccelerations)
        {
            var momentarilyAccelerations = new List<MomentarilyAcceleration>();

            foreach (var createMomentarilyAcceleration in createMomentarilyAccelerations)
            {
                var momentarilyAcceleration =
                    _mapper.Map<MomentarilyAcceleration>(createMomentarilyAcceleration);

                _repository.CreateMomentarilyAcceleration(momentarilyAcceleration);
                _repository.SaveChanges();

                momentarilyAccelerations.Add(momentarilyAcceleration);
            }

            _repository.SaveChanges();

            var calculateImpact = new CalculateImpact(momentarilyAccelerations, 74);
            var highestImpact = calculateImpact.CalculateAllImpacts().FirstOrDefault();
            var readImpact = _mapper.Map<ReadImpact>(highestImpact);
            return readImpact;
        }

        private bool TrainingSessionExists(int id)
        {
            if (_repository.GetTrainingSession(id) != null) return true;

            return false;
        }
    }
}