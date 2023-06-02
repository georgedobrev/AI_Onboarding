using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.JWTModels;
using AI_Onboarding.ViewModels.UserModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AI_Onboarding.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public IdentityService(IRepository<User> repository, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

        public async Task<(bool Success, string Message, TokenResponseViewModel? Tokens)> LoginAsync(UserLoginViewModel user)
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
    }
}