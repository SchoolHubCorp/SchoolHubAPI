using System.ComponentModel.DataAnnotations;

namespace SchoolHubApi.Models.UserDto;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}