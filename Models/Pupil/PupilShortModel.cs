using SchoolHubApi.Models.Classroom;
namespace SchoolHubApi.Models.Pupil
{
    public record PupilShortModel(
       int Id,
       string FirstName,
       string LastName,

       string Classname,
       int ClassroomId);
}
