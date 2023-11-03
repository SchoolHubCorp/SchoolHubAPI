using Microsoft.AspNetCore.Authorization;
using SchoolHubApi.Domain.Entities.Enums;

namespace SchoolHubApi.Controllers.Attributes
{
   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(params Role[] roles)
        {
            var a = String.Join(",", roles.Select(role => role.ToString()));
            Roles = roles.Contains(Role.Admin)? a: String.Join(",", nameof(Role.Admin), a);
        }
        public AuthAttribute()
        {

        }
    }
}
