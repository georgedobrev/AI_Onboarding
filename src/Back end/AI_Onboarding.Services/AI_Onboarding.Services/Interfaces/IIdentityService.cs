using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.UserModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IIdentityService : IService
    {
        public Task<(bool Success, string Message)> RegisterAsync(UserRegistrationViewModel user);
        public Task<(bool Success, string Message, TokenViewModel? Tokens)> LoginAsync(UserLoginViewModel user);
        public Task<(bool Success, string Message, TokenViewModel? Tokens)> GoogleLoginAsync(string token);
        public (bool Success, string Message, TokenViewModel? Tokens) RefreshTokenAsync(TokenViewModel tokens);
    }
}