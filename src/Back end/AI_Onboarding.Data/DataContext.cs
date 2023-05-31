using AI_Onboarding.Data.ModelBuilding;
using AI_Onboarding.Data.Models;
using AI_Onboarding.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Onboarding.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Repository<User> _repository;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor, Repository<User> repository) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
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

                _ = int.TryParse(_httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userId);
                var user = _repository.Find(userId);

                if (user is not null)
                {
                    ((BaseEntity)entityEntry.Entity).ModifiedBy = user;
                    ((BaseEntity)entityEntry.Entity).ModifiedById = userId;
                }
            }

            return base.SaveChanges();
        }
    }
}