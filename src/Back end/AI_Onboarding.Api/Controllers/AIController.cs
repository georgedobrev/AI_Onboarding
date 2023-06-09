using System;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Services.Interfaces;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly IConfiguration _configuration;

        public AIController(IAIService aiService, IConfiguration configuration)
        {
            _aiService = aiService;
            _configuration = configuration;
        }

        [HttpPost("receive-response")]
        public IActionResult GenerateResponse([FromBody] string question)
        {
            string output = _aiService.RunScript(_configuration["PythonScript:GenerateResponsePath"], question);

            return Content(output);
        }

        [HttpGet("train-model")]
        public IActionResult TrainModel()
        {
            string output = _aiService.RunScript(_configuration["PythonScript:TrainModelPath"], "The next olympics are in 2024");

            return Content(output);
        }
    }
}

