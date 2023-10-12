using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class CourseClass
    {
        public string CourseClassId { get; set; }
        
        public ICollection<Homework> Homeworks { get; } = new List<Homework>();
        public List<ClassCoursePupilGrade> ClassCoursePupilGrades { get; } = new();
    }
}