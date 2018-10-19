using EmailService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace EmailService.Services
{
    public class SendGridProvider : IEmailProvider
    {
        private AppSettingsModel _settings;

        public SendGridProvider(AppSettingsModel settings)
        {
            _settings = settings;
        }

        public async Task<bool> Send(EmailViewModel email)
        {
            
            var apiKey = _settings.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(_settings.SenderAddress, _settings.SenderName);
            var tos = new List<EmailAddress>();
            
            foreach (var recipient in email.To.Split(','))
            {
                tos.Add(new EmailAddress(email.To));
            }

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, email.Subject, email.Text, null);

            try
            {
                var response = await client.SendEmailAsync(msg);
                // TODO: need to log the error when the response status is not 200

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // TODO: log the error message either physically or Azure table storage
                return false;
            }
        }
    }
}
