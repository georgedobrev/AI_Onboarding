using System.Text;
using AI_Onboarding.Common;
using AI_Onboarding.Data.NoSQLDatabase.Interfaces;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using AI_Onboarding.ViewModels.ResponseModels;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SautinSoft.Document;
using Spire.Presentation;
using Xceed.Words.NET;
using IShape = Spire.Presentation.IShape;

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
                case (int)FileType.pptx:
                    using (Presentation presentation = new Presentation())
                    {
                        presentation.LoadFromStream(document.File.OpenReadStream(), Spire.Presentation.FileFormat.Auto);

                        foreach (ISlide slide in presentation.Slides)
                        {
                            foreach (IShape shape in slide.Shapes)
                            {
                                if (shape is IAutoShape)
                                {
                                    foreach (TextParagraph tp in (shape as IAutoShape).TextFrame.Paragraphs)
                                    {
                                        sb.AppendLine(tp.Text);
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            return sb.ToString();
        }

        public BaseResponseViewModel UploadFile(DocumentViewModel document)
        {
            if (document.File is null || document.File.Length == 0)
            {
                return new BaseResponseViewModel { Success = false, ErrorMessage = "No file uploaded." };
            }

            var extractedText = ExtractText(document);

            ScriptResponseViewModel resultStoreDocument;
            try
            {
                Data.Models.Document dbDocument = new Data.Models.Document { ExtractedText = extractedText };
                _documentRepository.Add(dbDocument);
                resultStoreDocument = _aiService.RunScript(_configuration["PythonScript:StoreDocumentPath"], extractedText);

                if (resultStoreDocument.Success)
                {
                    return new BaseResponseViewModel { Success = true, ErrorMessage = "" };
                }
                else
                {
                    return new BaseResponseViewModel { Success = false, ErrorMessage = resultStoreDocument.ErrorMessage };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return new BaseResponseViewModel { Success = false, ErrorMessage = ex.Message };
            }
        }

        public ConvertFileResponseViewModel ConvertToPdf(DocumentViewModel document)
        {
            switch (document.FileTypeId)
            {
                case (int)FileType.docx:
                    return new ConvertFileResponseViewModel { Success = true, ErrorMessage = "", ConvertedFile = ConvertDocxToPdf(document.File) };
                case (int)FileType.pptx:
                    return new ConvertFileResponseViewModel { Success = true, ErrorMessage = "", ConvertedFile = ConvertPptxToPdf(document.File) };
                default:
                    return new ConvertFileResponseViewModel { Success = true, ErrorMessage = "Unsupported document type ID." };
            }
        }

        private byte[] ConvertDocxToPdf(IFormFile docxFile)
        {
            using (var docxStream = docxFile.OpenReadStream())
            {
                var docxDocument = DocumentCore.Load(docxStream, new DocxLoadOptions());

                using (var pdfStream = new MemoryStream())
                {
                    docxDocument.Save(pdfStream, new PdfSaveOptions());
                    return pdfStream.ToArray();
                }
            }
        }

        private byte[] ConvertPptxToPdf(IFormFile pptxFile)
        {
            using var stream = new MemoryStream();
            pptxFile.CopyTo(stream);
            stream.Position = 0;

            var presentation = new Presentation();

            presentation.LoadFromStream(stream, FileFormat.Pptx2013);

            using var pdfStream = new MemoryStream();
            presentation.SaveToFile(pdfStream, FileFormat.PDF);

            return pdfStream.ToArray();
        }
    }
}


