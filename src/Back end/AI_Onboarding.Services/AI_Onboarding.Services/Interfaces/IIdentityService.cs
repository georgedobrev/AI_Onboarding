using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.UserModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IIdentityService : IService
    {
        public Task<(bool Success, string Message)> RegisterAsync(UserRegistrationViewModel user);
    }
}