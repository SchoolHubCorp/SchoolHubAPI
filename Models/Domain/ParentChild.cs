using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace SchoolHubApi.Models.Domain
{
    public class ParentChild
    {
        public int ParentChildId { get; set; }

        public User Child { get; set; }
        public User Parent { get; set; }
    }

}
