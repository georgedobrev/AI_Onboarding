using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.UserModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AI_Onboarding.Services.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public IdentityService(IRepository<User> repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
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
                    return (true, messages);
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
    }
}