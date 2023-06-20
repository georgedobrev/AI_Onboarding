using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.Services.Abstract;

namespace AI_Onboarding.Services.Interfaces
{
    public interface ITokenService : IService
    {
        public TokenViewModel GenerateAccessToken(string email, string name, int id, string[] roleNames, bool isLogin = false);
    }
}