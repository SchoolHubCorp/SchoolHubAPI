namespace SchoolHubApi.Domain.Entities;

public class Pupil : Entity
{
    public string AccessCode { get; set; }     
    public ICollection<Parent> Parents { get; set; }
    
    public UserData UserData { get; set; }
}