using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.ResponseModels;
using AI_Onboarding.ViewModels.UserModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IIdentityService : IService
    {
        public Task<BaseResponseViewModel> RegisterAsync(UserRegistrationViewModel user);
        public Task<TokensResponseViewModel> LoginAsync(UserLoginViewModel user);
        public Task<TokensResponseViewModel> GoogleLoginAsync(string token);
        public TokensResponseViewModel RefreshTokenAsync(TokenViewModel tokens);

    }
}