using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Models.DTO;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Controllers;

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
    public async Task<List<UserDto>> GetUser()
    {
        return await userRepository
            .GetAll()
            .Select(x => new UserDto()
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName
            })
            .ToListAsync();
    }

    //    private static List<UserDto> AdaptUser(List<User> usersFromDb)
    //    {
    //        List<UserDto> responseUsersList = new();

    //        UserDto? users;

    //        foreach (User user in usersFromDb)
    //        {
    //            users = new()
    //            {
    //                Id = user.Id,
    //                Name = user.Name,
    //                LastName = user.LastName,
    //            };
    //            responseUsersList.Add(users);
    //        }

    //        return responseUsersList;
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> CreateUser(CreateUserRequestDto request)
    //    {
    //        var user = new User
    //        {
    //            Name = request.Name,
    //            LastName = request.LastName,
    //        };

    //        await userRepository.CreateAsync(user);

    //        var response = new UserDto
    //        {
    //            Id = user.Id,
    //            Name = user.Name,
    //            LastName = user.LastName,
    //        };

    //        return Ok(response);
    //    }
    //}
}