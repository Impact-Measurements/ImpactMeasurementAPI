using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ImpactMeasurementAPI.Models;
using ImpactMeasurementAPI.DTOs;

namespace ImpactMeasurementAPI.Repositories
{
    public interface IAthleteRepository
    {
        Task<IdentityResult> SignUpAsync(Athlete signUpAthlete);
        Task<string> LoginAsync(AthleteSignIn athleteSignIn);
    }
}