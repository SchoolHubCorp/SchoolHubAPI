namespace SchoolHubApi.Models.Topic
{
    public record TopicSendFileModel
    (
        int Id,
        string TopicName,
        string TopicDescription,
        byte[] TeacherFile,
        string TeacherFileType
   );
}
