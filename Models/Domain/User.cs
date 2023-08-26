using Microsoft.AspNetCore.Mvc;

namespace SchoolHubApi.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
