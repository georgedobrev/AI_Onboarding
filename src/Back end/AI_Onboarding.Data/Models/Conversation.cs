using System;

namespace AI_Onboarding.Data.Models
{
    public class Conversation : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public User User { get; set; }
    }
}