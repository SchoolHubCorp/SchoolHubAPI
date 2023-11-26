using SchoolHubApi.Models.Homework;
using SchoolHubApi.Models.Topic;

namespace SchoolHubApi.Models.Course
{
    public record CourseTopicSendModel(
    int Id,
    string CourseName,
    List<TopicSendFileModel> Topics,
    List<HomeworkPupilModel> Homeworks
    );
}
