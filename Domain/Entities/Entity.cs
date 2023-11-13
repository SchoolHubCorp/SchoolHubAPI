using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolHubApi.Domain.Entities;

public class Entity
{
    [Key]
    public int Id { get; set; }
}