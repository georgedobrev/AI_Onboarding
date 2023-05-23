using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace AI_Onboarding.Data.Repository
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IdentityDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(IdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> Remove(int id)
        {
            T? entity = await GetByIdAsync(id);
            if (entity is null)
            {
                return null;
            }

            EntityEntry<T> entry = _entities.Remove(entity);
            return entry.Entity;
        }
    }
}

