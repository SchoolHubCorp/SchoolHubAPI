namespace SchoolHubApi.Domain.Entities;

public class Pupil : Entity
{
    public ICollection<Parent> Parents { get; set; }
    
    public UserData UserData { get; set; }
}