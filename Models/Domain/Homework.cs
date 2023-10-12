using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Homework
    {
        public int HomeworkId { get; set; }
        public byte[] HomeworkFile { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public List<HomeworkPupil> HomeworkPupils { get; } = new();
    }
}