using SchoolHubApi.Models.DTO;
using SchoolHubApi.Models.Domain;
namespace SchoolHubApi.Services
{
    public interface IAccountService 
    {
        void RegisterUser(UserDto dto);
    }
    public class AccountService : IAccountService
    {
        public void RegisterUser(UserDto dto) 
        {
            var newUser = new User()
            {
                Email = dto.Email
            };
        }
    }
}
