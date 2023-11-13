using SchoolHubApi.Migrations;
using SchoolHubApi.Models.Course;

namespace SchoolHubApi.Models.Teacher
{
    public record TeacherDetailModel(
        int Id,
        string Email,
        string FirstName,
        string LastName,
        List<CourseModel> Courses
        );
}
