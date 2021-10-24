using Fundamentos.Azure.ServiceBus.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fundamentos.Azure.ServiceBus.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class EmailController : ControllerBase
    {
        private readonly string root;
        private readonly string endpointHttpTrigger;
        private readonly string connectionString;
        private readonly string queueMailName;
        public EmailController(IConfiguration config, IWebHostEnvironment env)
        {
            root = env.ContentRootPath;
            endpointHttpTrigger = config.GetValue<string>("EndpointHttpTrigger");
            connectionString = config.GetValue<string>("AzureServiceBus");
            queueMailName = config.GetValue<string>("QueueMailName");
        }

        [HttpPost("emails")]
        public async Task<IActionResult> PostEmail(MailMessage mail)
        {
            var client = new QueueClient(connectionString, queueMailName);

            if (mail.BodyWithFile)
            {
                var path = $"{root}\\mail.html";
                using (StreamReader reader = new StreamReader(path))
                    mail.Body = reader.ReadToEnd();
            }

            var messageBody = JsonSerializer.Serialize(mail);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();

            return Ok(mail);
        }

        [HttpPost("emails-trigger")]
        public async Task<IActionResult> PostMailTrigger(MailMessage mail)
        {
            using (var client = new HttpClient())
            {
                var body = JsonSerializer.Serialize(mail);
                var content = new StringContent(body);
                await client.PostAsync(endpointHttpTrigger, content);
            }

            return Ok(mail);
        }
    }
}
