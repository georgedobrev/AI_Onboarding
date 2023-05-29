using AI_Onboarding.Data.Models;
using AI_Onboarding.ViewModels.JWTModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponse CreateToken(User user);
    }
}

