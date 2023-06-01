using System;
using System.ComponentModel.DataAnnotations;

namespace AI_Onboarding.ViewModels.UserModels
{
	public class UserRegistrationViewModel
	{
		[Required (ErrorMessage = "First name is required!")]
		public string FirstName { get; set; }

		[Required (ErrorMessage = "Last name is required!")]
		public string LastName { get; set; }

		[Required (ErrorMessage = "Email is required!")]
		[EmailAddress(ErrorMessage = "Invalid email address!")]
		public string Email { get; set; }

		[Required (ErrorMessage = "Password is required!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

