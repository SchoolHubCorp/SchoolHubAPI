namespace SchoolHubApi.Models.Pupil
{
    public record PupilHomeworkModel
    (
        string pupilName,
        string pupilSurname,
        byte[]? pupilHomework,
        int? MarkName
        );
}
