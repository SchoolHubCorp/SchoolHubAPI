
using SchoolHubApi.Models.EmailDto;

namespace SchoolHubApi.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
    }
}
