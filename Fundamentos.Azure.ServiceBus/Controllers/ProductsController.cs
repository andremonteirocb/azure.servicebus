using Fundamentos.Azure.ServiceBus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fundamentos.Azure.ServiceBus.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly string connectionString;
        private readonly string queueName;
        private readonly string topicName;
        public ProductsController(IConfiguration config)
        {
            this.config = config;
            connectionString = this.config.GetValue<string>("AzureServiceBus");
            queueName = this.config.GetValue<string>("QueueName");
            topicName = this.config.GetValue<string>("TopicName");
        }

        [HttpPost("queue")]
        public async Task<IActionResult> PostQueue(Product product)
        {
            await SendMessageQueue(product);
            return Ok(product);
        }

        private async Task SendMessageQueue(Product product)
        {
            var client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);
            string messageBody = JsonSerializer.Serialize(product);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();
        }

        [HttpPost("topic")]
        public async Task<IActionResult> PostTopic(Product product)
        {
            await SendMessageToTopic(product);
            return Ok(product);
        }

        private async Task SendMessageToTopic(Product product)
        {
            var client = new TopicClient(connectionString, topicName);
            string messageBody = JsonSerializer.Serialize(product);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();
        }

    }
}
