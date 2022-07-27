using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Model;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail
{
    public class MailService : IEmailSender
    {
        public EmailSettings _EmailSettings { get; set; }
        public ILogger<MailService> _Logger { get; set; }

        public MailService(EmailSettings emailSettings, ILogger<MailService> logger)
        {
            _EmailSettings = emailSettings;
            _Logger = logger;
        }
        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_EmailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailbody = email.Body;
            var from = new EmailAddress()
            {
                Email = _EmailSettings.FromAddress,
                Name = _EmailSettings.FromName
            };
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailbody, emailbody);
            var response = await client.SendEmailAsync(sendGridMessage);

            _Logger.LogInformation("Email sent.");

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
