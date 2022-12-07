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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _repository;
        
        public UserController(IUserRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        [HttpGet("users/all", Name = "GetUsers")]
        public IEnumerable<ReadUser> GetAllUsers()
        {

            var userModel = _repository.GetAllUsers();
            var users = _mapper.Map<IEnumerable<ReadUser>>(userModel);

            return users;
        }
        
        [HttpPost("users/create", Name = "CreateUser")]
        public ActionResult<ReadUser> CreateUser(CreateUser createUser)
        {
            var userModel = _mapper.Map<User>(createUser);
            _repository.CreateUser(userModel);
            _repository.SaveChanges();

            var userReadDto = _mapper.Map<ReadUser>(userModel);

            return userReadDto;
        }
        
        [HttpPut("users/minimum/threshold", Name = "UpdateMinimumImpactThreshold")]
        public ActionResult<ReadUser> UpdateMinimumImpactThreshold(UpdateMinimumImpactThreshold minimumImpactThreshold)
        {
            User user = _repository.GetUserById(minimumImpactThreshold.userId);
            user.MinimumImpactThreshold = minimumImpactThreshold.ImpactForce;
            _repository.SaveChanges();
            return _mapper.Map<ReadUser>(user);
        }

    }
}