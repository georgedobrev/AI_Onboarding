using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf.parser;
using Xceed.Document.NET;
using System.Diagnostics;
using AI_Onboarding.Common;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Administrator)]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload-file")]
        public ActionResult UploadFile([FromForm] DocumentViewModel document)
        {
            var result = _documentService.UploadDocument(document);

            if (result.Success)
            {
                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}