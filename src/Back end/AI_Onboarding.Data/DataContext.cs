using AI_Onboarding.Data.ModelBuilding;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Onboarding.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
            var entities = ChangeTracker.Entries();

            var currentDate = DateTime.UtcNow;

            int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);

            foreach (var entityEntry in entities)
            {
                var entity = entityEntry.Entity as IBaseEntity;

                if (entity is null)
                {
                    continue;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = currentDate;
                }
                else
                {
                    if (userId > 0)
                    {
                        entity.ModifiedById = userId;
                    }

                    entity.ModifiedAt = currentDate;
                }
            }

            return base.SaveChanges();
        }
    }
}