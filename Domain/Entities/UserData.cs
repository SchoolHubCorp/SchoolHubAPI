using System.ComponentModel.DataAnnotations;
using SchoolHubApi.Domain.Entities.Enums;

namespace SchoolHubApi.Domain.Entities;

public class UserData
{
    [Key]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public string Pesel { get; set; }

    public Role Role { get; set; }
    public ResetPasswordCode? ResetPasswordCode { get; set; }

    public Pupil? Pupil { get; set; }
    public Parent? Parent { get; set; }
    public Teacher? Teacher { get; set; }
}