using AI_Onboarding.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class UserConfigurator : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        //builder.Property(e => e.WorkPhone).HasMaxLength(50);

        builder.Property(e => e.Email).HasMaxLength(100).IsRequired();

        builder.Property(e => e.PhoneNumber).HasMaxLength(50).IsRequired();

        builder.HasIndex(e => e.Email).IsUnique();

        //builder.HasOne(x => x.Country)
        //       .WithMany(x => x.Users)
        //       .HasForeignKey(x => x.CountryId)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.Tenant)
        //       .WithMany(x => x.Users)
        //       .HasForeignKey(x => x.TenantId)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.EmployeeStatus)
        //       .WithMany(x => x.Users)
        //       .HasForeignKey(x => x.EmployeeStatusId)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.Source)
        //       .WithMany(x => x.Users)
        //       .HasForeignKey(x => x.SourceId)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.ModifiedBy)
        //       .WithMany(x => x.ModifiedUsers)
        //       .HasForeignKey(x => x.ModifiedById)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(x => x.DeletedBy)
        //       .WithMany(x => x.DeletedUsers)
        //       .HasForeignKey(x => x.DeletedById)
        //       .OnDelete(DeleteBehavior.Restrict);
    }
}