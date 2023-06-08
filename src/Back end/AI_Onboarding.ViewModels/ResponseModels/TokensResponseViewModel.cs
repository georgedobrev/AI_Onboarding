using System;
using AI_Onboarding.ViewModels.JWTModels;

namespace AI_Onboarding.ViewModels.ResponseModels
{
    public class TokensResponseViewModel : BaseResponseViewModel
    {
        public TokenViewModel? Tokens { get; set; }
    }
}

