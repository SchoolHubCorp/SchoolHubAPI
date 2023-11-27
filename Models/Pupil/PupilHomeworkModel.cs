namespace SchoolHubApi.Models.Pupil
{
    public record PupilHomeworkModel
    (
        int pupilId,
        string pupilName,
        string pupilSurname,
        int? HomeworkId,
        byte[]? pupilHomework,
        int? MarkName
        );
}
