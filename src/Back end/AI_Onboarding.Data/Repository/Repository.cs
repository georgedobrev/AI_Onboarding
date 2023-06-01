using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace AI_Onboarding.Data.Repository
{

    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly IdentityDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(IdentityDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public T? Find(int id)
        {
            T? entity = _entities.Find(id);
            if (entity is null)
            {
                return null;
            }

            return entity;
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public T? FindByCondition(Expression<Func<T, bool>> predicate)
        {
            T? entity = _entities.FirstOrDefault(predicate);
            if (entity is null)
            {
                return null;
            }

            return entity;
        }

        public T? Remove(int id)
        {
            T? entity = Find(id);
            if (entity is null)
            {
                return null;
            }

            return entity;
        }

        public void Update(T obj)
        {
            _entities.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}

