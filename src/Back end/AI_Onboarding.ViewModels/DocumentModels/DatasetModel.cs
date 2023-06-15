using System;
namespace AI_Onboarding.ViewModels.DocumentModels
{
    public class DatasetModel
    {
        public string DocumentText { get; set; } = string.Empty;
        public List<QuestionAnswer> QuestionAnswer { get; set; }
    }
}

