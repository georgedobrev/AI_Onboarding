using AI_Onboarding.Data.Models;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.JWTModels;
using System.Security.Claims;

namespace AI_Onboarding.Services.Interfaces
{
    public interface ITokenService : IService
    {
        TokenResponseViewModel GenerateAccessToken(TokenRequestViewModel user);

        string GenerateRefreshToken();
    }
}