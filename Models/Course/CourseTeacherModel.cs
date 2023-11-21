namespace SchoolHubApi.Models.Course
{
    public record CourseTeacherModel(
        int Id,
        string CourseName,
        string TeacherName,
        string TeacherLastName
        );
}
