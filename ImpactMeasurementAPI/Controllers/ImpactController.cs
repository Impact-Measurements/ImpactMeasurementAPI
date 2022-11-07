using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("trainingSessionId", Name = "GetAllFreeAccelerations")]
        public ActionResult<TrainingSession> GetAllFreeAccelerationValuesFromSession(int trainingSessionId)
        {
            var trainingSession = _repository.GetTrainingSession(trainingSessionId);

            return Ok(trainingSession);
        }
        
    }
}