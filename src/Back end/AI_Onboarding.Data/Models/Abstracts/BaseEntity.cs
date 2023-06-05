namespace AI_Onboarding.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public int? ModifiedById { get; set; }
        public User? ModifiedBy { get; set; }
    }
}