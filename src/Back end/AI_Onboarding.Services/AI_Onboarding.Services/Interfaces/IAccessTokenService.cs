using AI_Onboarding.Data.Models;
using AI_Onboarding.ViewModels.JWTModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IAccessTokenService
    {
        TokenResponse CreateToken(TokenRequest user);
    }
}

