namespace SchoolHubApi.Domain.Entities;

public class Teacher : Entity
{
    public string UserDataEmail { get; set; }
    public UserData UserData { get; set; }
}