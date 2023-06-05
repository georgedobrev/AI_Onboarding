using System.Text;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Xceed.Words.NET;

namespace AI_Onboarding.Services.Implementation
{
    public class DocumentService : IDocumentService
    {
        public DocumentService()
        {
        }

        public (bool Success, string Message) UploadDocument(DocumentViewModel document)
        {
            if (document.File is null || document.File.Length == 0)
            {
                return (false, "No file uploaded.");
            }

            StringBuilder sb = new StringBuilder();

            switch (document.FileType)
            {
                case FileType.pdf:
                    using (PdfReader reader = new PdfReader(document.File.OpenReadStream()))
                    {
                        for (int page = 1; page <= reader.NumberOfPages; page++)
                        {
                            string text = PdfTextExtractor.GetTextFromPage(reader, page);
                            sb.AppendLine(text);
                        }
                    }
                    break;

                case FileType.docx:
                    using (DocX doc = DocX.Load(document.File.OpenReadStream()))
                    {
                        foreach (var paragraph in doc.Paragraphs)
                        {
                            sb.AppendLine(paragraph.Text);
                        }
                    }
                    break;
            }


            string extractedText = sb.ToString();

            return (true, "Success");
        }
    }
}

