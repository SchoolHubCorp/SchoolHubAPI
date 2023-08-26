using SchoolHubApi.Models.Domain;

namespace SchoolHubApi.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<List<User>> GetAsync();
    }
}
