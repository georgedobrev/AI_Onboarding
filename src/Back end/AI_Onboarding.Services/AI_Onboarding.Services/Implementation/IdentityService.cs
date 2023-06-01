using System;
using System.Security.Claims;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using AI_Onboarding.Services.Interfaces;
using AI_Onboarding.ViewModels.UserModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

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

        public async Task<bool> RegisterAsync(UserRegistrationViewModel viewUser)
        {
            var user = _mapper.Map<User>(viewUser);

            user.SecurityStamp = Guid.NewGuid().ToString();

            await _userManager.CreateAsync(user, viewUser.Password);

            _repository.Add(user);
            if (_repository.SaveChanges())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

