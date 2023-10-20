namespace SchoolHubApi.Models.EmailDto
{
    public class EmailRequest
    {
        public string? ToEmail { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
