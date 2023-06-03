using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.UserModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AI_Onboarding.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public IdentityService(IRepository<User> repository, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenService tokenService, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(UserRegistrationViewModel viewUser)
        {
            var messages = "";

            var user = _mapper.Map<User>(viewUser);
            try
            {
                var result = await _userManager.CreateAsync(user, viewUser.Password);

                if (result.Succeeded)
                {
                    return (true, "Register succsefull");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        messages += $"{error.Description}%";
                    }

                    return (false, messages.Remove(messages.Length - 1));
                }
            }
            catch (Exception ex)
            {
                messages += $"{ex.Message}";
                return (false, messages);
            }
        }

        public async Task<(bool Success, string Message, TokenViewModel? Tokens)> LoginAsync(UserLoginViewModel user)
        {
            var messages = "";

            try
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

                if (result.Succeeded)
                {
                    int id = _repository.FindByCondition(u => u.Email == user.Email).Id;

                    return (true, "Login success", _tokenService.GenerateAccessToken(user.Email, id));
                }
                else
                {
                    return (false, "Wrong credentials", null);
                }
            }
            catch (Exception ex)
            {
                messages += $"{ex.Message}";
                return (false, messages, null);
            }
        }

        public (bool Success, string Message, TokenViewModel? Tokens) RefreshTokenAsync(TokenViewModel tokens)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;

                var principal = tokenHandler.ValidateToken(tokens.Token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                }, out securityToken);
                var validatedToken = securityToken as JwtSecurityToken;

                if (validatedToken?.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return (false, "Invalid algorithm", null);
                }

                var nameIdentifier = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

                var user = _repository.FindByCondition(u => u.Id == nameIdentifier && u.RefreshToken == Uri.UnescapeDataString(tokens.RefreshToken));

                if (user is null || user?.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    return (false, "Invalid token", null);
                }

                var newTokens = _tokenService.GenerateAccessToken(user.Email, user.Id);
                return (true, "Successfully generated token", newTokens);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null);
            }
        }
    }
}