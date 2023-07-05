using System;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.ConversationModels;
using AI_Onboarding.ViewModels.ResponseModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IConversationService : IService
    {
        public int? AddToConversation(int? id, string question, string answer);
        public UserConversationsViewModel GetUserConversations();
        public ConversationInfo? GetConversation(int id);
        public BaseResponseViewModel DeleteConversation(int id);
    }
}

