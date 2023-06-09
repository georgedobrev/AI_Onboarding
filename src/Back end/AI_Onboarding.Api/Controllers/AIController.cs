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
            string output = _aiService.RunScript(_configuration["PythonScript:TrainModelPath"], @"
        [
            {
                ""context"": ""The Olympics is a major international sporting event..."",
                ""qas"": [
                    {
                        ""question"": ""What is the frequency of the Olympic Games?"",
                        ""answers"": [
                            {
                                ""text"": ""The Olympic Games take place every four years."",
                                ""answer_start"": 44
                            }
                        ]
                    },
                    {
                        ""question"": ""Which country hosted the ancient Olympic Games?"",
                        ""answers"": [
                            {
                                ""text"": ""The ancient Olympic Games were hosted by Greece."",
                                ""answer_start"": 94
                            }
                        ]
                    }
                ]
            }
        ]");

            return Content(output);
        }
    }
}

