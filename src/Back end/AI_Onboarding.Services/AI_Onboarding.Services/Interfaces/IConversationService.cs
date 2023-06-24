using System;
using AI_Onboarding.Services.Abstract;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IConversationService : IService
    {
        public void AddToConversation(int? id, string question, string answer);
    }
}

