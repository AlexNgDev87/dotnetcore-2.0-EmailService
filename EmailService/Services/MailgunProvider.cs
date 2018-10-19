using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using EmailService.Models;
using Microsoft.Extensions.Options;

namespace EmailService.Services
{
    public class MailgunProvider : IEmailProvider
    {
        private AppSettingsModel _settings;

        public MailgunProvider(AppSettingsModel settings)
        {
            _settings = settings;
        }

        public async Task<bool> Send(EmailViewModel email)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", _settings.MailgunApiKey);

            RestRequest request = new RestRequest();
            request.AddParameter("domain", _settings.MailgunDomainName, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"{_settings.SenderName} <{_settings.SenderAddress}>");

            foreach (var recipient in email.To.Split(','))
            {
                request.AddParameter("to", recipient);
            }
            
            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Text);
            request.Method = Method.POST;
            
            try
            {
                var response = await Task.Run(() => client.Execute(request));
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
