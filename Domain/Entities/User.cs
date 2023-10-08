using Domain.Models;

namespace Domain.Entities;

public class User : Entity
{
    public string Name { get; set; }
    public string LastName { get; set; }
}