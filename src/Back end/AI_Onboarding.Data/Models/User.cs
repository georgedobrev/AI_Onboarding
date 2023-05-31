﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AI_Onboarding.Data.Models
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public User ModifiedBy { get; set; }
        public int? ModifiedById { get; set; }
        public ICollection<RoleClaim> ModifiedRoleClaims { get; set; }
        public ICollection<Role> ModifiedRoles { get; set; }
        public ICollection<UserClaim> ModifiedUserClaims { get; set; }
        public ICollection<User> ModifiedUsers { get; set; }
        public ICollection<UserLogin> ModifiedUserLogins { get; set; }
        public ICollection<UserRole> ModifiedUserRoles { get; set; }
        public ICollection<UserToken> ModifiedUserTokens { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}