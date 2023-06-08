using AI_Onboarding.Services.Abstract;
using AI_Onboarding.ViewModels.DocumentModels;
using AI_Onboarding.ViewModels.ResponseModels;

namespace AI_Onboarding.Services.Interfaces
{
    public interface IDocumentService : IService
    {
        public BaseResponseViewModel UploadDocument(DocumentViewModel document);
    }
}

