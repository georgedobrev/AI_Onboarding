using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Onboarding.ViewModels.UserModels.PasswordResetModels
{
    public class SendEmailViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
