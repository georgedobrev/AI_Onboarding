using AI_Onboarding.Data.ModelBuilding;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AI_Onboarding.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly User _currentUser;
        public DataContext(DbContextOptions<DataContext> options, User currentUser) : base(options)
        {
            _currentUser = currentUser;
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

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentDate = DateTime.UtcNow;

            foreach (var entityEntry in entities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = currentDate;
                }

                ((BaseEntity)entityEntry.Entity).ModifiedAt = currentDate;

                if (_currentUser is not null)
                {
                    ((BaseEntity)entityEntry.Entity).ModifiedBy = _currentUser;
                    ((BaseEntity)entityEntry.Entity).ModifiedById = _currentUser.Id;
                }
            }

            return base.SaveChanges();
        }
    }
}