using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SchoolHubApi.Domain.Entities;

namespace SchoolHubApi.Helpers;

public static class UserHelper
{
    public static string GenerateJwtToken(this IConfiguration configuration, UserData userData)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userData.Username),
            new Claim(ClaimTypes.Role,  userData.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetSection("Authentication:JwtKey").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: cred);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}