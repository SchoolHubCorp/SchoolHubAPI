using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation;

public class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    private readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }
    
    public IQueryable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }
    
    public IQueryable<T> FindWithTracking(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsTracking();
    }
    
    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
