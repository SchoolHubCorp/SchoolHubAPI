using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SchoolHubApi.Models.EmailDto;

namespace SchoolHubApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;

        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            if (mailRequest.ToEmail is not null)
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            else
                email.To.Add(MailboxAddress.Parse(_mailSettings.AdminMail));
            email.Subject = mailRequest.Title;
            var builder = new BodyBuilder();

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
