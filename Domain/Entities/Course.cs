namespace SchoolHubApi.Domain.Entities
{
    public class Course : Entity
    {
        public string CourseName { get; set; }
        public int ClassroomId { get; set; }
        public int TeacherId { get; set; }
        public Classroom Classroom { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Topic> Topic { get; set; }
        private Course()
        {

        }
        public Course(string courseName)
        {
            CourseName = courseName;
        }
    }
}
