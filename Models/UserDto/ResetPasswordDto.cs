using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SchoolHubApi.Models.UserDto
{
    public record ResetPasswordDto(
        [EmailAddress] string Email,
        string AccessCode,
        string Password
        );
    
}
