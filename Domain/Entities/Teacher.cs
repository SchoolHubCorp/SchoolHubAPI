namespace SchoolHubApi.Domain.Entities;

public class Teacher : Entity
{
    public byte[]? TeacherPlan { get; set; }
    public string? TeacherPlanContentType { get; set; }
    public ICollection<Course> Courses { get; set; }

    public string UserDataEmail { get; set; }
    public UserData UserData { get; set; }
}