using System;
using AutoMapper;
using AI_Onboarding.Data.Models.UserRegistration;
using AI_Onboarding.Data.Models;
namespace AI_Onboarding.Data.AutoMapperProfiles
{
	public class MapingProfile : Profile
	{
		public MapingProfile()
		{
			CreateMap<UserRegistrationModel, User>()
				.ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));

		}
	}
}

