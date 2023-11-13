namespace SchoolHubApi.Models.Teacher
{
    public record TeacherShortModel(
        int Id,
        string Email,
        string FirstName,
        string LastName
    );
}