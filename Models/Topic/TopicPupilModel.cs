namespace SchoolHubApi.Models.Topic
{
    public record TopicPupilModel
    (
        int TopicId,
        string TopicName,
        string TopicDescription,
        byte[] TeacherFile,
        string TeacherFileType
        );

}
