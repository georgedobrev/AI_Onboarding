using System;
using AI_Onboarding.Data.Models.UserRegistration;
using AI_Onboarding.
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AI_Onboarding.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register(UserRegistrationModel userModel)
		{

			var user = _mapper.Map<User>(userModel);
			var result = await _userManager.CreateAsync(user, userModel.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return StatusCode(400);
			}
			await _userManager.AddPasswordAsync(user, "Visitor");

			return Ok();
		}
	}
}

