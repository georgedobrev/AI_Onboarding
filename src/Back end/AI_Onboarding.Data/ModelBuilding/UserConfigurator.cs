﻿using AI_Onboarding.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class UserConfigurator : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(e => e.UserName).HasMaxLength(255);

        builder.Property(e => e.NormalizedUserName).HasMaxLength(255);

        builder.Property(e => e.Email).HasMaxLength(100).IsRequired();

        builder.Property(e => e.NormalizedEmail).HasMaxLength(100);

        builder.Property(e => e.PhoneNumber).HasMaxLength(50).IsRequired();

        builder.HasIndex(e => e.Email).IsUnique();

        builder.HasIndex(e => e.NormalizedEmail).IsUnique();
    }
}