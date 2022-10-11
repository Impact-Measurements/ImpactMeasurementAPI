using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.DTOs;
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

        public ActionResult<IEnumerable<ReadAccelerationOnAxes>> GetAllFreeAccelerationValuesFromSession(int trainingSessionId)
        {
            var trainingSession = _repository.GetAllFreeAccelerationValuesFromSession(trainingSessionId);
            
            return Ok(_mapper.Map<IEnumerable<ReadAccelerationOnAxes>>(trainingSession));
        }
    }
}