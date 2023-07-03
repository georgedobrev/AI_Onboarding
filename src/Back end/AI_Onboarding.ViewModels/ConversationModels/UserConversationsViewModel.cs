using System;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.ViewModels.ConversationModels
{
    public class UserConversationsViewModel
    {
        public List<ConversationDTO> Conversations { get; set; } = new List<ConversationDTO>();
    }
}

