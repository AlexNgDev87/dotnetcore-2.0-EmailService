using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Services
{
    internal interface IEmailProvider
    {
        Task<bool> Send(EmailViewModel email);
    }
}
