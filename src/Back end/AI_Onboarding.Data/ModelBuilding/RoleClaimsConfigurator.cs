using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AI_Onboarding.Data.Models;

namespace AI_Onboarding.Data.ModelBuilding
{
    public class RoleClaimsConfigurator : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable("RoleClaims");

            builder.HasOne(x => x.ModifiedBy)
            .WithMany(x => x.ModifiedRoleClaims)
            .HasForeignKey(x => x.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}