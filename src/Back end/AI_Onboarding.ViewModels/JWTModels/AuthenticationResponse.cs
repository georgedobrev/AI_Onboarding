namespace AI_Onboarding.ViewModels.JWTModels
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}

