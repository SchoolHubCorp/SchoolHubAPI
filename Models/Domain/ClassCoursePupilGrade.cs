using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class ClassCoursePupilGrade
    {
        public int ClassCoursePupilGradeId { get; set; }
        public double Grade { get; set; }
        public int Semestr { get; set; }

        public DateTime Data { get; set; }

        public List<HomeworkPupil> HomeworkPupils { get; } = new();
    }
}