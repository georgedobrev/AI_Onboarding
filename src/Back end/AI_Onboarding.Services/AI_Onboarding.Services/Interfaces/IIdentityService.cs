using System;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IIdentityService : IService
    {
        public Task<bool> RegisterAsync(UserRegistrationViewModel user);
    }
}

