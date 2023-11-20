using SchoolHubApi.Models.Course;
using SchoolHubApi.Models.Teacher;

namespace SchoolHubApi.Models.Classroom
{
    public record ClassroomCourseModel
    
        (int Id,
        string ClassName,
        string ClassAccessCode,
        List<CourseNameModel>Courses);
}
