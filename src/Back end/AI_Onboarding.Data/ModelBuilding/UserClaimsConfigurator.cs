using AI_Onboarding.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AI_Onboarding.Data.ModelBuilding
{
    public class UserClaimsConfigurator : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");

            builder.HasOne(x => x.ModifiedBy)
            .WithMany(x => x.ModifiedUserClaims)
            .HasForeignKey(x => x.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}