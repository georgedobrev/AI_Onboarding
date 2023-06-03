using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Services.Abstract;
using System.Security.Claims;
using AI_Onboarding.ViewModels.UserModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface ITokenService : IService
    {
        TokenViewModel GenerateAccessToken(string email, int id);
    }
}