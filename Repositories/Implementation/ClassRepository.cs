using SchoolHubApi.Data;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Repositories.Implementation
{
    public class ClassRepository : BaseRepository<Classroom>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
