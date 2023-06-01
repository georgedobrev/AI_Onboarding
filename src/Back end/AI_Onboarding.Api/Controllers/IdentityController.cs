using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Services.Interfaces;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityServise;

        public AccountController(IIdentityService identityServise)
        {
            _identityServise = identityServise;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationViewModel userModel)
        {
            if (await _identityServise.RegisterAsync(userModel))
            {
                return Ok();
            }
            else
            {
                return StatusCode(400);
            }
        }
    }
}