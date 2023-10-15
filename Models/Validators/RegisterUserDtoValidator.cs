using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using SchoolHubApi.Data;
using SchoolHubApi.Models.DTO;

namespace SchoolHubApi.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<UserDto>
    {
        public RegisterUserDtoValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}