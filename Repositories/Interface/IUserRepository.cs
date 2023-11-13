using System.Linq.Expressions;
using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Repositories.Interface
{
    public interface IUserRepository 
    {
        IQueryable<UserData> Find(Expression<Func<UserData, bool>> expression);
        IQueryable<UserData> FindWithTracking(Expression<Func<UserData, bool>> expression);
        Task SaveChangesAsync();
        void Remove(UserData userData);
    }
}
