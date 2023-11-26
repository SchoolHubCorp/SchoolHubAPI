using SchoolHubApi.Models.Homework;

namespace SchoolHubApi.Models.Topic
{  public record TopicFileModel
    (
    int Id,
    string TopicName,
    string TopicDescription,
    List<TopicSendFileModel> Topics,
    List<HomeworkPupilModel> Homeworks
    );
}
