using System;

namespace Fundamentos.Azure.ServiceBus.Models
{
    public class MailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool BodyWithFile { get; set; }
    }
}
