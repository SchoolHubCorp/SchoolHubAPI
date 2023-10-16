using System.Linq.Expressions;
using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Repositories.Interface;

public interface IBaseRepository<T> where T : Entity
{
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> expression);
    IQueryable<T> FindWithTracking(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void Remove(T entity);
    
    void SaveChanges();
    Task SaveChangesAsync();
}