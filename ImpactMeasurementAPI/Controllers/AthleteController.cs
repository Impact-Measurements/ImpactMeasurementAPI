using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ImpactMeasurementAPI.Data;
using ImpactMeasurementAPI.DTOs;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAthleteRepo _repository;
        
        public AthleteController(IAthleteRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet("users/all", Name = "GetUsers")]
        public IEnumerable<ReadAthlete> GetAllUsers()
        {

            var userModel = _repository.GetAllUsers();
            var users = _mapper.Map<IEnumerable<ReadAthlete>>(userModel);

            return users;
        }
        
        [HttpPost("users/create", Name = "CreateUser")]
        public ActionResult<ReadAthlete> CreateUser(CreateAthlete createUser)
        {
            var userModel = _mapper.Map<Athlete>(createUser);
            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            var userReadDto = _mapper.Map<ReadAthlete>(userModel);

            return userReadDto;
        }
        
        [HttpPut("users/minimum/threshold", Name = "UpdateMinimumImpactThreshold")]
        public ActionResult<ReadAthlete> UpdateMinimumImpactThreshold(UpdateMinimumImpactThreshold minimumImpactThreshold)
        {
            Athlete user = _repository.GetUserById(minimumImpactThreshold.userId);
            user.MinimumImpactThreshold = minimumImpactThreshold.ImpactForce;
            _repository.SaveChanges();
            return _mapper.Map<ReadAthlete>(user);
        }

    }
}