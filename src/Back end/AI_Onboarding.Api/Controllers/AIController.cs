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
            var result = _aiService.RunScript(_configuration["PythonScript:GenerateResponsePath"], question);

            if (result.Success)
            {
                return Ok(result.Output);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }

        }

        [HttpPost("train-model")]
        public IActionResult TrainModel()
        {
            var result = _aiService.RunScript(_configuration["PythonScript:TrainModelPath"], @"[
    {
        ""document_id"": 1,
        ""document_text"": ""The sun is a star."",
        ""questions"": [
            {
                ""question_id"": 1,
                ""question_text"": ""What is the sun?"",
                ""answers"": ""A star.""       
            }
        ]
    },
    {
        ""document_id"": 2,
        ""document_text"": ""Water is essential for life."",
        ""questions"": [
            {
                ""question_id"": 2,
                ""question_text"": ""What is essential for life?"",
                ""answers"": ""Water""
            }
        ]
    }
]
");

            if (result.Success)
            {
                return Ok(result.Output);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}

