using SchoolHubApi.Domain.Entities;
using SchoolHubApi.Models.Classroom;
using SchoolHubApi.Models.Parent;

namespace SchoolHubApi.Models.Pupil
{
    public record PupilDetailModel(
        int Id,
        string Email,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Pesel,
        string ParentAccessCode,

        string Classname,
        int ClassroomId,

        List<ParentModel>Parents);
}
