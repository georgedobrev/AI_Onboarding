namespace AI_Onboarding.Data.Models
{
    public interface IBaseEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        string ModifiedBy { get; set; }
    }
}

