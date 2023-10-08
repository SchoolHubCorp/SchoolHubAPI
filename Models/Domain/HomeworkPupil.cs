using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class HomeworkPupil
    {
        public string HomeworkPupilId { get; set; }
        public byte[] File { get; set; }
        public int HomeworkHomeworId { get; set; }
        public int PupilPupilId { get; set; }
        public int CourseClassPupilGradesClassId { get; set; }

        public ClassCoursePupilGrades ClassCoursePupilGrades { get; set; } = null!;
        public Pupil Pupil { get; set; } = null!;
        public Homework Homework { get; set; } = null!;
    }
}