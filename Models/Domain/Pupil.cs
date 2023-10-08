using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Pupil
    {
        public int PupilId { get; set; }
        public int UserUserId { get; set; }
        public int ClassClassId { get; set; }

        public List<HomeworkPupil> HomeworkPupils { get; } = new();

        public User User { get; set; } = null!;
        public Class Class { get; set; } = null!;

        public List<ClassCoursePupilGrades> ClassCoursePupilGradeses { get; } = new();
    }
}