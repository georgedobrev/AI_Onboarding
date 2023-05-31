using System;
using AI_Onboarding.Data.Models.UserRegistration;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace AI_Onboarding.Api.Controllers
{
	public class AccountController : Controller
	{
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost("Register")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(UserRegistrationModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return View(userModel);
			}
			var user = _mapper.Map<User>(userModel);
			var result = await _userManager.CreateAsync(user, userModel.Password);

			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.TryAddModelError(error.Code, error.Description);
				}
				return View(userModel);
			}
			await _userManager.AddPasswordAsync(user, "Visitor");

			return RedirectToAction("Index", "Dashboard");
		}

		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;

		public AccountController(IMapper mapper, UserManager<User> userManager)
		{
			_mapper = mapper;
			_userManager = userManager;
		}
	}
}

