﻿using System;
using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.ConversationModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IConversationService : IService
    {
        public void AddToConversation(int? id, string question, string answer);
        public UserConversationsViewModel GetUserConversations();
    }
}
