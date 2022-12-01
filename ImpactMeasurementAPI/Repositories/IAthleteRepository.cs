namespace ImpactMeasurementAPI.Repositories
{
    public interface IAthleteRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
        Task<string> LoginAsync(SignInModel signInModel);
    }
}