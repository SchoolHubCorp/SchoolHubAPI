using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class UserRole
    {
        public int UserRoleId { get; set; }

        public User User { get; set; } = null!;
        public Role Role { get; set; } = null!;
    }
}