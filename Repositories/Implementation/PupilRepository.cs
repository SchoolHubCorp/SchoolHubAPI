using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation;

public class PupilRepository : BaseRepository<Pupil>, IPupilRepository
{
    public PupilRepository(ApplicationDbContext context) : base(context)
    {
    }
}