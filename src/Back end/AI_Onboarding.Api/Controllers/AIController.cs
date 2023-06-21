﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.Common;

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

        [HttpPost("response")]
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
    }
}

