namespace SchoolHubApi.Domain.Entities
{
    public class Topic : Entity
    {
        public string TopicName { get; set; }
        public string Description { get; set; }
        public byte[]? TopicFile { get; set; }
        public string? TopicFileType { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Homework> Homeworks { get; set; }
        private Topic() 
        {
        }
        public Topic(string topicName)
        {
            TopicName = topicName;
        }
    }
}
