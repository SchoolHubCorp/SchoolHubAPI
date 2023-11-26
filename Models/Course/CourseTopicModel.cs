using SchoolHubApi.Models.Topic;

namespace SchoolHubApi.Models.Course
{
    public record CourseTopicModel(
        int Id,
        string CourseName,
        List<TopicPupilModel> Topics
        );
}
