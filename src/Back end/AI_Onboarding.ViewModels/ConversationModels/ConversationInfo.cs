using System;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.ViewModels.ConversationModels
{
    public class ConversationInfo
    {
        public int Id { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}

