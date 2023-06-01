﻿using System;
using AutoMapper;
using AI_Onboarding.ViewModels.UserModels;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.ViewModels.UserModels.UserProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationViewModel, User>()
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(x => x.Password));


        }
    }
}

