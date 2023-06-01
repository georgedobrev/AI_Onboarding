using AI_Onboarding;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Services.Interfaces;
using System.Security.Claims;

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

