﻿namespace AI_Onboarding.Data.Models
{
    public class BaseEntity : IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public User ModifiedBy { get; set; }
        public int? ModifiedById { get; set; }
    }
}