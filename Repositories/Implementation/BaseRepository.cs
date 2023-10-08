using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Repositories.Interface;
using System.Linq.Expressions;

namespace SchoolHubApi.Repositories.Implementation;

public class BaseRepository<T> : IBaseRepository<T> where T : Entity
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _repositoryContext;

    protected BaseRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _repositoryContext = dbContext.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        return _repositoryContext.AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _repositoryContext.Where(expression).AsNoTracking();
    }

    public async Task<T?> AddAsync(T? entity)
    {
        if (entity is null) return default;
        if (Exists(entity.Id)) return entity;

        var entityEntry = await _repositoryContext.AddAsync(entity);
        return entityEntry.Entity;
    }

    public T? Add(T? entity)
    {
        if (entity is null) return default;
        if (Exists(entity.Id)) return entity;

        var entityEntry = _repositoryContext.Add(entity);
        return entityEntry.Entity;
    }

    public T? Update(T? entity)
    {
        if (entity is null) return default;
        if (Exists(entity.Id)) return entity;

        _repositoryContext.Update(entity);
        return entity;
    }

    public bool Exists(Guid id)
    {
        return _repositoryContext.Any(x => x.Id == id);
    }

    public void Delete(T entity)
    {
        if (!Exists(entity.Id)) return;

        _repositoryContext.Remove(entity);
    }
}
