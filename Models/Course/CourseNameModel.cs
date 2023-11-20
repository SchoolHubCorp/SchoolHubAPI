using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.Teacher;

namespace SchoolHubApi.Models.Course
{
    public record CourseNameModel
    (
        int Id,
        string CourseName,
        string TeacherName,
        string TeacherLastName);
}
