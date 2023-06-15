using System.Text;
using AI_Onboarding.Common;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.NoSQLDatabase;
using AI_Onboarding.Data.NoSQLDatabase.Interfaces;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using AI_Onboarding.ViewModels.ResponseModels;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xceed.Words.NET;

namespace AI_Onboarding.Services.Implementation
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<DocumentService> _logger;
        private readonly IAIService _aiService;
        private readonly IConfiguration _configuration;

        public DocumentService(IDocumentRepository documentRepository, ILogger<DocumentService> logger, IAIService aiService, IConfiguration configuration)
        {
            _documentRepository = documentRepository;
            _logger = logger;
            _aiService = aiService;
            _configuration = configuration;
        }


        private string ExtractText(DocumentViewModel document)
        {
            StringBuilder sb = new StringBuilder();

            switch (document.FileTypeId)
            {
                case (int)FileType.pdf:
                    using (PdfReader reader = new PdfReader(document.File.OpenReadStream()))
                    {
                        for (int page = 1; page <= reader.NumberOfPages; page++)
                        {
                            string text = PdfTextExtractor.GetTextFromPage(reader, page);
                            sb.AppendLine(text);
                        }
                    }
                    break;

                case (int)FileType.docx:
                    using (DocX doc = DocX.Load(document.File.OpenReadStream()))
                    {
                        foreach (var paragraph in doc.Paragraphs)
                        {
                            sb.AppendLine(paragraph.Text);
                        }
                    }
                    break;
            }

            return sb.ToString();
        }


        public BaseResponseViewModel UploadDocument(DocumentViewModel document)
        {
            if (document.File is null || document.File.Length == 0)
            {
                return new BaseResponseViewModel { Success = false, ErrorMessage = "No file uploaded." };
            }

            var extractedText = ExtractText(document);

            ScriptResponseViewModel result;
            try
            {
                Document dbDocument = new Document { ExtractedText = extractedText };
                _documentRepository.Add(dbDocument);
                result = _aiService.RunScript(_configuration["PythonScript:StoreDocumentPath"], extractedText);

                if (document.QuestionsAnswers is not null)
                {
                    var datasetObject = new DatasetModel { DocumentText = extractedText, QuestionAnswer = document.QuestionsAnswers };
                    string datasetString = JsonConvert.SerializeObject(datasetObject, Formatting.Indented);

                    return new BaseResponseViewModel
                    {
                        Success = true,
                        ErrorMessage = datasetString
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return new BaseResponseViewModel { Success = false, ErrorMessage = ex.Message };
            }

            if (result.Success)
            {
                return new BaseResponseViewModel { Success = true, ErrorMessage = "" };
            }
            else
            {
                return new BaseResponseViewModel { Success = false, ErrorMessage = result.ErrorMessage };
            }
        }
    }
}

