using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class ClassCoursePupilGrades
    {
        public int ClassCoursePupilGradesId { get; set; }
        public double Grade { get; set; }
        public int Semestr { get; set; }
        public int GradeNameGradesId { get; set; }
        public string CourseClassCourseClassId { get; set; }
        public int PupilPupilId { get; set; }
        public DateTime Data { get; set; }

        public List<HomeworkPupil> HomeworkPupils { get; } = new();

        public GradeName GradeName { get; set; } = null!;
        public  CourseClass CourseClass { get; set; } = null!;
        public Pupil Pupil { get; set; } = null!;
    }
}