using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.UserDto;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation;

public class ParentRepository : BaseRepository<Parent>, IParentRepository
{
    public ParentRepository(ApplicationDbContext context) : base(context)
    {
    }
}