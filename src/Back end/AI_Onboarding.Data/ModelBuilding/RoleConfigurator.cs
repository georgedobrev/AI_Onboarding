using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Onboarding.Data.ModelBuilding
{
    public class RoleConfigurator : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("Roles");

            builder.Property(e => e.Name).HasMaxLength(255);

            builder.Property(e => e.NormalizedName).HasMaxLength(255);
        }
    }
}