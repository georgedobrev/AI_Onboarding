using System;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.ViewModels.ConversationModels
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}

