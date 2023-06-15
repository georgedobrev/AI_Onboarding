using Microsoft.AspNetCore.Http;

namespace AI_Onboarding.ViewModels.DocumentModels
{
    public class DocumentViewModel
    {
        public IFormFile File { get; set; }
        public int FileTypeId { get; set; }
        public List<QuestionAnswer>? QuestionsAnswers { get; set; }
    }
}

