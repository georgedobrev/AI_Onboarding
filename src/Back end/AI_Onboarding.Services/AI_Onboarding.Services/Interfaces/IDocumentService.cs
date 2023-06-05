using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.DocumentModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IDocumentService : IService
    {
        public (bool Success, string Message) UploadDocument(DocumentViewModel document);
    }
}

