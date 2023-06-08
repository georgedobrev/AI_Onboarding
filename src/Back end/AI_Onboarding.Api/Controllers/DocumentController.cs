using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.DocumentModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf.parser;
using Xceed.Document.NET;
using System.Diagnostics;

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

        [HttpPost("upload-file")]
        public ActionResult UploadFile([FromBody] DocumentViewModel document)
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

        [HttpPost("python-test")]
        public IActionResult RunPythonScript([FromBody] string question)
        {
            string scriptPath = "/Users/hristo.chipev/Documents/Projects/PythonTestModels/main.py";
            string argument = $"\"{question}\"";

            // Create a new process to run the Python script
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/opt/homebrew/Cellar/python@3.11/3.11.3/bin/python3",
                    Arguments = $"{scriptPath} {argument}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            // Start the process
            process.Start();

            // Read the output of the Python script
            string output = process.StandardOutput.ReadToEnd();

            // Wait for the process to finish
            process.WaitForExit();

            return Content(output);
        }
    }
}