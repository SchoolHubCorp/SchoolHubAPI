using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class CourseClass
    {
        public string CourseClassId { get; set; }
        public int CourseCourseId { get; set; }
        public int ClassClassId { get; set; }
        public int UserUserId { get; set; }

        public User User { get; set; } = null!;
        public Class Class { get; set; } = null!;
        public Course Course { get; set; } = null!;

        public ICollection<Homework> Homeworks { get; } = new List<Homework>();

        public List<ClassCoursePupilGrades> ClassCoursePupilGradeses { get; } = new();
    }
}