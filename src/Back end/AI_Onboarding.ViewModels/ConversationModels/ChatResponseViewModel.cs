using System;
namespace AI_Onboarding.ViewModels.ConversationModels
{
    public class ChatResponseViewModel
    {
        public int? Id { get; set; }
        public string Answer { get; set; } = string.Empty;
    }
}