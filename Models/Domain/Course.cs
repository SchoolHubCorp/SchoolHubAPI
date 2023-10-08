using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }

        public List<CourseClass> CourseClasses { get; } = new();
    }
}