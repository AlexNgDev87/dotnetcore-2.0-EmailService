using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class AppSettingsModel
    {
        public string MailgunApiKey { get; set; }
        public string MailgunDomainName { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SendGridApiKey { get; set; }
    }
}
