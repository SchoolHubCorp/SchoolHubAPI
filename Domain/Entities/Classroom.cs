using System.Reflection.Metadata;

namespace SchoolHubApi.Domain.Entities;

public class Classroom : Entity
{
    public string ClassName { get; set; }
    public byte[]? Plan { get; set; }
    public string? PlanContentType { get; set; }
    public string ClassAccessCode { get; set; }
    public ICollection<Pupil> Pupils { get; set; }
    private Classroom()
    {
        
    }
    public Classroom(string name)
    {
        ClassName = name;
    }
}