using System;
using AI_Onboarding.Services.Abstract;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IAIService : IService
    {
        public string RunScript(string relativePath, string argument);
    }
}

