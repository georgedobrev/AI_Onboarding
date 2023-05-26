using AI_Onboarding.Data.ModelBuilding;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AI_Onboarding.Data
{
    public class DataContext : IdentityDbContext<User, Role, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserConfigurator().Configure(modelBuilder.Entity<User>());
            new UserRolesConfigurator().Configure(modelBuilder.Entity<UserRole>());
            new UserClaimsConfigurator().Configure(modelBuilder.Entity<UserClaim>());
            new UserLoginsConfigurator().Configure(modelBuilder.Entity<UserLogin>());
            new UserTokensConfigurator().Configure(modelBuilder.Entity<UserToken>());
            new RoleConfigurator().Configure(modelBuilder.Entity<Role>());
            new RoleClaimsConfigurator().Configure(modelBuilder.Entity<RoleClaim>());
        }
    }
}