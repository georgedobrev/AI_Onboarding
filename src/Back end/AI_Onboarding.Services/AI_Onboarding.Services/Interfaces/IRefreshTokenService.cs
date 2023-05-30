using System;
namespace AI_Onboarding.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateToken(string username);
    }
}

