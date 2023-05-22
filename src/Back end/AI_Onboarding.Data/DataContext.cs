using AI_Onboarding.Data.ModelBuilding;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AI_Onboarding.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfigurator().Configure(modelBuilder.Entity<User>());
            new UserRolesConfigurator().Configure(modelBuilder.Entity<IdentityUserRole<string>>());
            new UserClaimsConfigurator().Configure(modelBuilder.Entity<IdentityUserClaim<string>>());
            new UserLoginsConfigurator().Configure(modelBuilder.Entity<IdentityUserLogin<string>>());
            new UserTokensConfigurator().Configure(modelBuilder.Entity<IdentityUserToken<string>>());
            new RoleConfigurator().Configure(modelBuilder.Entity<IdentityRole>());
            new RoleClaimsConfigurator().Configure(modelBuilder.Entity<IdentityRoleClaim<string>>());
        }
    }
}