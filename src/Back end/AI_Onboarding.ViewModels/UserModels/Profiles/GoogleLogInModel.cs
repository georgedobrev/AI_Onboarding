using Microsoft.AspNetCore.Authentication;
using System;
namespace AI_Onboarding.ViewModels.UserModels.Profiles
{
	public class GoogleLogInModel
	{
		public string Email { get; set; }

		public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}

