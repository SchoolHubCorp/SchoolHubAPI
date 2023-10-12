using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class GradeName
    {
        public int GradeNameId { get; set; }
        public string Name { get; set; }

        public List<ClassCoursePupilGrade> ClassCoursePupilGrades { get; } = new();
    }
}