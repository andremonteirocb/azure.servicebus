using System;

namespace Fundamentos.Azure.ServiceBus.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
