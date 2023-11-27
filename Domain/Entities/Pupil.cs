namespace SchoolHubApi.Domain.Entities;

public class Pupil : Entity
{
    public string AccessCode { get; set; }
    public int ClassroomId { get; set; }
    public ICollection<Parent> Parents { get; set; }
    public Classroom Classroom { get; set; }
    public ICollection<Homework> Homeworks { get; set; }
    public ICollection<Mark> Marks { get; set; }

    public string UserDataEmail { get; set; }
    public UserData UserData { get; set; }
}