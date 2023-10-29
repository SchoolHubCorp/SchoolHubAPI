using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Models.UserDto;

public class AuthenticateResponse
{
    public string Email { get; set; }

    public string Token { get; set; }

    public string Role { get; set; }
    public AuthenticateResponse(UserData userData, string token)
    {
        Email = userData.Email;
        Token = token;
        Role = userData.Role.ToString();
    }
}