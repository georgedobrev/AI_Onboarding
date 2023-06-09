using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        [HttpPost("receive-response")]
        public IActionResult RunPythonScript([FromBody] string question)
        {
            string scriptPath = @"../AI_Onboarding.Services/AI_Onboarding.Services/PythonScripts/GenerateResponse.py";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPythonFilePath = System.IO.Path.Combine(baseDirectory, scriptPath);

            string argument = $"\"{question}\"";

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

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            return Content(output);
        }
    }
}

