using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ImpactMeasurementAPI.Data;

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
        
        
    }
}