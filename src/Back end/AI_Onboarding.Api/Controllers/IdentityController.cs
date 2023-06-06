using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using Microsoft.AspNetCore.Identity;
using AI_Onboarding.ViewModels.UserModels.Profiles;
using AI_Onboarding.Data.Models;
using System.Security.Claims;

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
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel userModel)
        {
            var result = await _identityServise.LoginAsync(userModel);
            if (result.Success)
            {
                Response.Cookies.Append("Access-Token", result.Tokens.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("Refresh-Token", result.Tokens.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost("refreshToken")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody] TokenViewModel tokensModel)
        {
            var result = _identityServise.RefreshTokenAsync(tokensModel);
            if (result.Success)
            {
                int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                Response.Cookies.Append("Access-Token", result.Tokens.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("Refresh-Token", result.Tokens.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            GoogleLogInModel googleLogInModel = new GoogleLogInModel
            {
                ReturnUrl = returnUrl
               
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Errpr from external provider: {remoteError}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest , new {Message = "External LogIn info not found!"});
            }

            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = new User { UserName = info.Principal.FindFirstValue(ClaimTypes.Email), Email = info.Principal.FindFirstValue(ClaimTypes.Email) };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        var randomPasswordHash = _userManager.PasswordHasher.HashPassword(user, Guid.NewGuid().ToString());
                        user.PasswordHash = randomPasswordHash;
                        await _userManager.UpdateAsync(user);
                    }
                }

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new {Message = "User registration failed." });
                }
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Ok();
            }
        }

       
    }
}