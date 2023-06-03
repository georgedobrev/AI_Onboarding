using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;

namespace AI_Onboarding.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityServise;
        private readonly IConfiguration _configuration;

        public IdentityController(IIdentityService identityServise, IConfiguration configuration)
        {
            _identityServise = identityServise;
            _configuration = configuration;
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
                Response.Cookies.Append("Access-Token", result.Tokens.Token);
                Response.Cookies.Append("Refresh-Token", result.Tokens.RefreshToken);

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
                    Expires = DateTimeOffset.UtcNow.AddMinutes(tokenValidityInMinutes),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                Response.Cookies.Append("Refresh-Token", result.Tokens.RefreshToken, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(refreshTokenValidityInDays),
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
    }
}