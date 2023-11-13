namespace SchoolHubApi.Models.Course
{
    public record CourseDto(
        string CourseName,
        int ClassroomId,
        int TeacherId
        );
    
}