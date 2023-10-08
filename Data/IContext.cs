using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace SchoolHubApi.Data
{
    public interface IContext : IAsyncDisposable, IDisposable
    {
        public DatabaseFacade Database { get; }
        public DbSet<TEntity> Set<TEntity>() where TEntity : class;
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        public DbSet<User> Users { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
