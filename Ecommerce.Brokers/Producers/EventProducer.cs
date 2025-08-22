using Ecommerce.Brokers.Abstractions;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Ecommerce.Brokers.Producers
{
    public class EventProducer : Broker, IEventProducer
    {
        public EventProducer(ConnectionFactory factory) : base(factory) { }
     
        public async Task SendAsync<TQueue>(TQueue message, CancellationToken token = default) where TQueue : class
        {
            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await CreateExchangeAsync<TQueue>(token);

            await PublishAsync<TQueue>(messageBody, token);
        }
    }
}
