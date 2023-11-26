namespace SchoolHubApi.Models.Teacher
{
public record TeacherFileModel
     (
    int Id,
    byte[] TeacherFile,
    string TeacherFileType
    );
}
