using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.UserDto;

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
            var newUser = new UserData()
            {
                Email = dto.Email
            };
        }
    }
}
