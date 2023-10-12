using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class HomeworkPupil
    {
        public string HomeworkPupilId { get; set; }
        public byte[] File { get; set; }
        public Pupil Pupil { get; set; }
    }
}