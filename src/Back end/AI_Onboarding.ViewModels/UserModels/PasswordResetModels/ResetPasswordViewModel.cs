using System;
namespace AI_Onboarding.ViewModels.UserModels.PasswordResetModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}

