using AI_Onboarding.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet("all")]
        public ActionResult GetAllConversations()
        {
            var conversations = _conversationService.GetUserConversations();

            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public ActionResult GetConversation(int id)
        {
            var conversation = _conversationService.GetConversation(id);

            if (conversation is not null)
            {

                return Ok(conversation);
            }
            else
            {
                return NotFound("Conversation doesn't exist!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteConversation(int id)
        {
            var result = _conversationService.DeleteConversation(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
