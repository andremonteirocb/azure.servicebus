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
    public class ProdutosController : ControllerBase
    {
        private readonly string connectionString;
        private readonly string queueName;
        private readonly string topicName;
        public ProdutosController(IConfiguration config)
        {
            connectionString = config.GetValue<string>("AzureServiceBus");
            queueName = config.GetValue<string>("QueueName");
            topicName = config.GetValue<string>("TopicName");
        }

        [HttpPost("queue")]
        public async Task<IActionResult> PostQueue(Produto product)
        {
            var client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);
            var messageBody = JsonSerializer.Serialize(product);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();

            return Ok(product);
        }

        [HttpPost("topic")]
        public async Task<IActionResult> PostTopic(Produto product)
        {
            var client = new TopicClient(connectionString, topicName);
            var messageBody = JsonSerializer.Serialize(product);
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await client.SendAsync(message);
            await client.CloseAsync();

            return Ok(product);
        }
    }
}
