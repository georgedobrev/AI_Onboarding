﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.Data.ModelBuilding
{
    public class UserRolesConfigurator : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasOne(x => x.ModifiedBy)
            .WithMany(x => x.ModifiedUserRoles)
            .HasForeignKey(x => x.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}