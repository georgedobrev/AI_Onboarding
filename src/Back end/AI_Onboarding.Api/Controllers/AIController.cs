using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.ConversationModels;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly IConversationService _conversationService;
        private readonly IConfiguration _configuration;

        public AIController(IAIService aiService, IConfiguration configuration, IConversationService conversationService)
        {
            _aiService = aiService;
            _configuration = configuration;
            _conversationService = conversationService;
        }

        [HttpPost("response")]
        public IActionResult GenerateResponse([FromBody] ConversationViewModel conversation)
        {
            var result = _aiService.RunScript(_configuration["PythonScript:GenerateResponsePath"], conversation.Question);

            if (result.Success)
            {
                var id = _conversationService.AddToConversation(conversation.Id, conversation.Question, result.Output);

                return Ok(new ChatResponseViewModel { Id = id, Answer = result.Output });
            }
            else
            {
                var id = _conversationService.AddToConversation(conversation.Id, conversation.Question, result.ErrorMessage);

                return BadRequest(new ChatResponseViewModel { Id = id, Answer = result.ErrorMessage });
            }
        }
    }
}

