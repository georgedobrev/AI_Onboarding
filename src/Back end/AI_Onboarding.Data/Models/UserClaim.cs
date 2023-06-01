using Microsoft.AspNetCore.Identity;

namespace AI_Onboarding.Data.Models
{
    public class UserClaim : IdentityUserClaim<int>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public User ModifiedBy { get; set; }
        public int? ModifiedById { get; set; }
    }
}