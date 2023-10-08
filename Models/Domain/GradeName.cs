using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class GradeName
    {
        public int GradeTypeId { get; set; }
        public string Type { get; set; }

        public List<ClassCoursePupilGrades> ClassCoursePupilGradeses { get; } = new();
    }
}