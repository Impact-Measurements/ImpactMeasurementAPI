using System;
using System.Threading.Tasks;
using ImpactMeasurementAPI.Logic;
using ImpactMeasurementAPI.Models;
using ImpactMeasurementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ImpactMeasurementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IAthleteRepository _athleteRepository;

        public AthleteController(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Athlete signUpAthlete)
        {
            var result = await _athleteRepository.SignUpAsync(signUpAthlete);

            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AthleteSignIn athleteSignIn)
        {
            var result = await _athleteRepository.LoginAsync(athleteSignIn);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}