using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using Microsoft.AspNetCore.Identity;
using AI_Onboarding.Data.Models;
using System.Security.Claims;
using SendGrid;
using System.ComponentModel.DataAnnotations;
using AI_Onboarding.ViewModels.UserModels.TokenValidationModel;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityServise;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public IdentityController(IIdentityService identityServise, IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _identityServise = identityServise;
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegistrationViewModel userModel)
        {
            var result = await _identityServise.RegisterAsync(userModel);
            if (result.Success)
            {
                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel userModel)
        {
            var result = await _identityServise.LoginAsync(userModel);
            if (result.Success)
            {
                Response.Headers.Add("Access-Token", result.Tokens.Token);
                Response.Headers.Add("Refresh-Token", result.Tokens.RefreshToken);

                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> SendPasswordResetEmailAsync([FromBody] string email)
        {
            var result = await _identityServise.SendPasswordResetEmailAsync(email);
            if (result.Success)
            {
                return Ok("Password reset email sent successfully.");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("validate-reset-token")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateResetToken([FromBody] ValidateTokenViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var isTokenValid = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", model.Token);

            if (isTokenValid)
            {
                return Ok("Token is valid!");
            }
            else
            {
                return BadRequest("Token is invalid or expired!");
            }
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody] TokenViewModel tokensModel)
        {
            var result = _identityServise.RefreshTokenAsync(tokensModel);
            if (result.Success)
            {
                int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                Response.Headers.Add("Access-Token", result.Tokens.Token);

                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("google-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLoginAsync([FromBody] string token)
        {
            var result = await _identityServise.GoogleLoginAsync(token);
            if (result.Success)
            {
                int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                Response.Headers.Add("Access-Token", result.Tokens.Token);
                Response.Headers.Add("Refresh-Token", result.Tokens.RefreshToken);

                return Ok(result.ErrorMessage);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}