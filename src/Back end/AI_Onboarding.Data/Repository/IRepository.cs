using AI_Onboarding.Data.Models;
using System.Linq.Expressions;

namespace AI_Onboarding.Data.Repository
{
    public interface IRepository<T> where T : IBaseEntity
    {
        IQueryable<T> GetAll();
        T? Find(int id);
        void Add(T entity);
        T? Remove(int id);
        bool SaveChanges();
        T? FindByCondition(Expression<Func<T, bool>> predicate);
    }
}

