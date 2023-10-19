using System.ComponentModel.DataAnnotations;

namespace SchoolHubApi.Models.UserDto;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}