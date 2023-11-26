namespace SchoolHubApi.Models.Homework
{
    public record HomeworkPupilModel
(
    int HomeworkId,
    byte[] PupilFile,
    string PupilFileType
);
}
