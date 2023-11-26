namespace SchoolHubApi.Domain.Entities
{
    public class Homework : Entity
    {
        public byte[]? HomeworkFile { get; set; }
        public string? HomeworkFileType { get; set; }
        public int TopicId { get; set; }
        public int PupilId { get; set; }
        public Topic Topic { get; set; }
        public Mark Mark { get; set; }
        public Pupil Pupil { get; set; }
    }
}
