namespace SchoolHubApi.Models.Teacher
{
    public record TeacherShortModel(
        int Id,
        string FirstName,
        string LastName,
        int QuantityOfsubjects
    );
}