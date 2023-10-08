using System.Linq.Expressions;
using System.Threading;

namespace SchoolHubApi.Repositories.Interface
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T?> AddAsync(T entity);
        T? Add(T entity);
        T? Update(T entity);
        void Delete(T entity);
        bool Exists(Guid id);
    }
}
