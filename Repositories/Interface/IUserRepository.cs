using System.Linq.Expressions;
using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Repositories.Interface
{
    public interface IUserRepository 
    {
        IQueryable<UserData> Find(Expression<Func<UserData, bool>> expression);
    }
}
