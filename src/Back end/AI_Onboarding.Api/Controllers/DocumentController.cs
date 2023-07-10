using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.Common;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("upload")]
        public ActionResult UploadFile([FromForm] DocumentViewModel document)
        {
            var result = _documentService.UploadFile(document);

            if (result.Success)
            {
                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("convert")]
        public ActionResult ConvertFile([FromForm] DocumentViewModel document)
        {
            var result = _documentService.ConvertToPdf(document);

            if (result.Success)
            {
                return Ok(result.ConvertedFile);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}