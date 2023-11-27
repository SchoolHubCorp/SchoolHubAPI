using SchoolHubApi.Models.Mark;

namespace SchoolHubApi.Models.Course
{
    public record CourseWithMarks
        (
            string CourseName,
            List<MarkDto> Marks
        );

}
