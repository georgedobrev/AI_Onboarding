using Microsoft.AspNetCore.Identity;

namespace AI_Onboarding.Data.Models
{
    public class Role : IdentityRole<int>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}

