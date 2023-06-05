using System;
using Microsoft.AspNetCore.Http;

namespace AI_Onboarding.ViewModels.DocumentModels
{
    public enum FileType
    {
        pdf,
        docx,
    }

    public class DocumentViewModel
    {
        public IFormFile File { get; set; }
        public FileType FileType { get; set; }
    }
}

