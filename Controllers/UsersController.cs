using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Data;
using SchoolHubApi.Models.Domain;
using SchoolHubApi.Models.DTO;
using SchoolHubApi.Models.Validators;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<CreateUserRequestDto>> GetUser()
        {
            return AdaptUser(await userRepository.GetAsync());
        }

        private static List<CreateUserRequestDto> AdaptUser(List<User> usersFromDb)
        {
            List<CreateUserRequestDto> responseUsersList = new();

            CreateUserRequestDto? users;

            foreach (User user in usersFromDb)
            {
                users = new()
                {
                    Name = user.FirstName,
                    LastName = user.LastName,
                };
                responseUsersList.Add(users);
            }

            return responseUsersList;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
        {
            var user = new User
            {
                FirstName = request.Name,
                LastName = request.LastName,
            };

            await userRepository.CreateAsync(user);

            var response = new UserDto
            {
                Name = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(response);
        }
    }
}
