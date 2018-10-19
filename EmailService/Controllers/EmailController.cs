using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmailService.Controllers
{
    [Produces("application/json")]
    [Route("api/email")]
    public class EmailController : Controller
    {
        private IOptions<AppSettingsModel> _settings;

        public EmailController(IOptions<AppSettingsModel> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        [Route("getemail")]
        public IActionResult GetEmail()
        {
            return Ok("Simple Get");
        }

        // POST: api/email
        [HttpPost]
        [Route("send")]
        public async Task<IActionResult> Send([FromBody]EmailViewModel email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sender = new Sender(_settings.Value, email);
            var result = await sender.SendMessage();

            return Ok(result);
        }
    }
}
