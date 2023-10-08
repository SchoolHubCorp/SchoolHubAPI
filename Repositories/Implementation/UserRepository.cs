using Domain.Entities;
using SchoolHubApi.Data;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation;

public class UserRepository : BaseRepository<User>, IUserRepository
{

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public List<User> GetAsync()
    {
        return GetAll().ToList();
    }
}
