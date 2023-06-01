using System;
using AI_Onboarding.Services.Abstract;

namespace AI_Onboarding.Services.Interfaces
{
	public interface IIdentityService : IService
	{
		public void Register(UserRegistrationViewModel user);
	}
}

