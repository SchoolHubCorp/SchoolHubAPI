using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class Class
    {
        public int ClassId { get; set; }
        public string Classname { get; set; }
        public string Plan { get; set; }

        public List<Pupil> Pupils { get; } = new();
        public List<CourseClass> CourseClasses { get; } = new();


    }
}