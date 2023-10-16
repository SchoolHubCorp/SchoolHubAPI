using System.Linq.Expressions;
using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public IQueryable<UserData> Find(Expression<Func<UserData, bool>> expression)
    {
        return _context.Set<UserData>().Where(expression);
    }
}