using SchoolHubApi.Migrations;
using SchoolHubApi.Models.Course;

namespace SchoolHubApi.Models.Teacher
{
    public record TeacherDetailModel(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string Pesel,
        List<CourseClassModel> Courses
        );
}
