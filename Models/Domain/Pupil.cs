using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Pupil
    {
        public int PupilId { get; set; }
        public List<HomeworkPupil> HomeworkPupils { get; } = new();
        public List<ClassCoursePupilGrade> ClassCoursePupilGrades { get; } = new();
    }
}