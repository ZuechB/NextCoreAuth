using Microsoft.Extensions.Options;
using Models;
using Models.Exceptions;
using Models.Mail;
using Models.Users;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface IMailService
    {
        Task Send(IEnumerable<ApplicationUser> users, Template template, BaseEmail substitutions = null);
    }

    public class MailService : IMailService
    {
        readonly AppSettings appSettings;
        public MailService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        public async Task Send(IEnumerable<ApplicationUser> users, Template template, BaseEmail substitutions = null)
        {
            if (String.IsNullOrWhiteSpace(appSettings.SendGrid.FromEmail) || String.IsNullOrWhiteSpace(appSettings.SendGrid.APIKey) || String.IsNullOrWhiteSpace(appSettings.SendGrid.FromName))
            {
                throw new BadRequestException("Sendgrid is not setup, please provide your information within the appsettings");
            }

            var templateId = EmailSystemTemplates.GetTemplate(template);

            List<EmailAddress> addresses = null;
            addresses = users.Select(u => new EmailAddress()
            {
                Email = u.Email,
                Name = u.FirstName
            }).ToList();

            foreach (var to in addresses)
            {
                substitutions.email = to.Email;
                substitutions.firstname = to.Name;

                var email = new Email(appSettings.SendGrid.APIKey)
                {
                    From = new From
                    {
                        Name = appSettings.SendGrid.FromName
                    },
                    Substitutions = substitutions,
                    To = to,
                    HtmlContent = " ",
                    TemplateId = templateId
                };

                email.From.Email = appSettings.SendGrid.FromEmail;

                await SendToSendGrid(email);
            }
        }

        private async Task SendToSendGrid(Email email)
        {
            var client = new SendGridClient(email.ApiKey);

            var from = new EmailAddress(email.From.Email, email.From.Name);
            var msg = MailHelper.CreateSingleTemplateEmail(from,
                                                          email.To,
                                                          email.TemplateId,
                                                          email.Substitutions
                                                        );

            var response = await client.SendEmailAsync(msg);
        }
    }
}
