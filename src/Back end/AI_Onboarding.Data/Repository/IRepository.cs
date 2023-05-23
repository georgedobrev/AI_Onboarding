using AI_Onboarding.Data.Models;
using System.Linq.Expressions;

namespace AI_Onboarding.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        Task<T?> Remove(int id);
        Task<bool> SaveChangesAsync();
        Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}

