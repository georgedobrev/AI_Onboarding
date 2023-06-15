namespace AI_Onboarding.ViewModels.DocumentModels
{
    public class DatasetModel
    {
        public string DocumentText { get; set; } = string.Empty;
        public List<QuestionAnswer> QuestionsAnswers { get; set; }
    }
}

