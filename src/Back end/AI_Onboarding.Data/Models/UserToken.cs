using Microsoft.AspNetCore.Identity;

namespace AI_Onboarding.Data.Models
{
    public class UserToken : IdentityUserToken<int>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public User? ModifiedBy { get; set; }
        public int? ModifiedById { get; set; }
    }
}