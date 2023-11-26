using SchoolHubApi.Models.Homework;

namespace SchoolHubApi.Models.Topic
{
    public record TopicSendFileModel
    (
        int TopicId,
        string TopicName,
        string TopicDescription,
        byte[] TeacherFile,
        string TeacherFileType,
        List<HomeworkPupilModel> Homeworks
   );
}
