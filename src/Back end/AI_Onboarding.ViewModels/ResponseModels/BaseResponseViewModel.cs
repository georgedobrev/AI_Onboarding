using System;
namespace AI_Onboarding.ViewModels.ResponseModels
{
    public class BaseResponseViewModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}

