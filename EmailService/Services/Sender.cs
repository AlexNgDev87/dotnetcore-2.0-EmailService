using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmailService.Models;
using RestSharp;

namespace EmailService.Services
{
    public class Sender
    {
        private AppSettingsModel _settings;
        private EmailViewModel _email;

        public Sender(AppSettingsModel settings, EmailViewModel email)
        {
            _settings = settings;
            _email = email;
        }

        public async Task<bool> SendMessage()
        {
            var response = true;

            IEmailProvider provider = new MailgunProvider(_settings);
            response = await provider.Send(_email);

            // Switch to SendGrid if mailgun fail
            if (response == false)
            {
                provider = new SendGridProvider(_settings);
                response = await provider.Send(_email);
            }

            return response;
        }
    }
}
