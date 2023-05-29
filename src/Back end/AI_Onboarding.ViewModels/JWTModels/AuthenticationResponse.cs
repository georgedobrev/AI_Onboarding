namespace AI_Onboarding.ViewModels.JWTModels
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenValidTo { get; set; }
    }
}

