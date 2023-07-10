using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.ResponseModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IAIService : IService
    {
        public ScriptResponseViewModel RunScript(string relativePath, string argument = "");
    }
}

