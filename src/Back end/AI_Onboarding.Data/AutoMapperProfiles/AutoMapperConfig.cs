using System;
using AutoMapper;

namespace AI_Onboarding.Data.AutoMapperProfiles
{
	public class AutoMapperConfig
	{
		public static IMapper Configure()
		{
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapingProfile());
            });

            return config.CreateMapper();
        }
	}
}

