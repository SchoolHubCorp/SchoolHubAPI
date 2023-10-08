using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleId { get; set; }
        public List<UserRole> UserRoles { get; } = new();
    }
}