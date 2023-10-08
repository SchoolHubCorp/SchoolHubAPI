using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolHubApi.Models.Domain
{
    public class ParentChild
    {
        public int ParentChildId { get; set; }
        
        public int ParentUserId { get; set; }
        public User ParentUser { get; set; }

        public int ChildUserId { get; set; }
        public User ChildUser { get; set; }
    }
}