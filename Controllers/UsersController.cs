using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Domain.Entities.Enums;
using SchoolHubApi.Helpers;
using SchoolHubApi.Models.UserDto;
using SchoolHubApi.Repositories.Interface;

namespace SchoolHubApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPupilRepository _pupilRepository;
    private readonly IParentRepository _parentRepository;
    private readonly IConfiguration _configuration;

    public UsersController(IUserRepository userRepository, IPupilRepository pupilRepository, IConfiguration configuration, IParentRepository parentRepository)
    {
        _userRepository = userRepository;
        _pupilRepository = pupilRepository;
        _configuration = configuration;
        _parentRepository = parentRepository;
    }

    [HttpPost("register/pupil")]
    public async Task<ActionResult<AuthenticateResponse>> RegisterPupil([FromBody] PupilDto request)
    {
        if (_userRepository
            .Find(x => x.Email == request.Email)
            .Any())
            return Conflict("User with this email already exists.");


        var accessCode = AccessCodeGenerator.GenerateAccessCode((string code) => 
            _pupilRepository.Find(x => x.AccessCode == code).Any());

        HashPasswordHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var pupil = new Pupil()
        {
            AccessCode = accessCode,
            UserData = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Pesel = request.Pesel,
                Role = Role.Pupil
            }
        };

        _pupilRepository.Add(pupil);
        await _pupilRepository.SaveChangesAsync();

        return Login(new AuthenticateRequest
        {
            Email = request.Email,
            Password = request.Password
        });
    }

    [HttpPost("register/parent")]
    public async Task<ActionResult<AuthenticateResponse>> RegisterParent(ParentDto request)
    {
        if (_userRepository
            .Find(x => x.Email == request.Email)
            .Any())
            return Conflict("User with this email already exists.");

        HashPasswordHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var child = _pupilRepository
            .Find(x => x.AccessCode == request.ChildCode)
            .FirstOrDefault();

        if (child is null)
            return BadRequest("Incorrect child code");

        var parent = new Parent()
        {
            UserData = new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Pesel = request.Pesel,
                Role = Role.Parent
            }
        };

        parent.Children.Add(child);

        _parentRepository.Add(parent);
        await _parentRepository.SaveChangesAsync();

        return Login(new AuthenticateRequest
        {
            Email = request.Email,
            Password = request.Password
        });
    }

    [HttpPost("login")]
    public ActionResult<AuthenticateResponse> Login(AuthenticateRequest request)
    {
        var userInDb = _userRepository
            .Find(x => x.Email == request.Email)
            .FirstOrDefault();

        if (userInDb == null ||
            !HashPasswordHelper.VerifyPasswordHash(request.Password, userInDb.PasswordHash, userInDb.PasswordSalt))
        {
            return BadRequest("Email or password is incorrect.");
        }

        var token = _configuration.GenerateJwtToken(userInDb);

        return new AuthenticateResponse(userInDb, token);
    }

    [HttpGet("TestAuth"), Authorize]
    public ActionResult Test0()
    {
        ClaimsPrincipal currentUser = this.User;
        return Ok(" ");
    }

    [HttpGet("TestTeacher"), Authorize(Roles = nameof(Role.Teacher))]
    public ActionResult Test()
    {
        ClaimsPrincipal currentUser = this.User;
        return Ok("1");
    }

    [HttpGet("TestAdmin"), Authorize(Roles = nameof(Role.Admin))]
    public ActionResult Test1()
    {
        ClaimsPrincipal currentUser = this.User;
        return Ok("1");
    }

    [HttpGet("TestParent"), Authorize(Roles = nameof(Role.Parent))]
    public ActionResult Test2()
    {
        ClaimsPrincipal currentUser = this.User;
        return Ok("1");
    }

    [HttpGet("TestPupil"), Authorize(Roles = nameof(Role.Pupil))]
    public ActionResult Test3()
    {
        ClaimsPrincipal currentUser = this.User;
        return Ok("1");
    }
}