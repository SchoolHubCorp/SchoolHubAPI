using SchoolHubApi.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Domain.Entities;

public class ResetPasswordCode: Entity
{
    public string Email { get; set; }
    public string ResetCode { get; private set; }
    public DateTime ValidTo { get; private set; }

    public ResetPasswordCode()
    {
        ValidTo = DateTime.Now.AddHours(2);
        ResetCode = AccessCodeGenerator.GenerateAccessCode();
    }
}