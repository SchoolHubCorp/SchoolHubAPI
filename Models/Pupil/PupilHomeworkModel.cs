namespace SchoolHubApi.Models.Pupil
{
    public record PupilHomeworkModel
    (
        string pupilName,
        string pupilSurname,
        int? HomeworkId,
        byte[]? pupilHomework,
        int? MarkName
        );
}
