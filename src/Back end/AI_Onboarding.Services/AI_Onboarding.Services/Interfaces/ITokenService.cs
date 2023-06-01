using AI_Onboarding.Data.Models;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.JWTModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface ITokenService : IService
    {
        TokenResponse GenerateAccessToken(TokenRequest user);

        string GenerateRefreshToken(string username);
    }
}