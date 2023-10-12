using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Plan { get; set; }
        public int AccessCode { get; set; }
        public int Pesel { get; set; }

        public ICollection<ParentChild> Children { get; set; } 
        public ICollection<ParentChild> Parents { get; set; }
        public List<UserRole> UserRoles { get; } = new();
        public List<CourseClass> CourseClasses { get; } = new();
        public List<Pupil> Pupils { get; } = new();
    }
}