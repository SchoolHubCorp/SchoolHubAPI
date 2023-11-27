using SchoolHubApi.Models.Mark;

namespace SchoolHubApi.Models.Pupil
{
    public record PupilWithMarks
        (
            string PupilName,
            string PupilSurname,
            List<MarkDto>Marks
        );
}
