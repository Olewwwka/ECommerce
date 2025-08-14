using Ecommerce.Brokers.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ecommerce.Brokers.Consumers
{
    public class EventConsumer<TQueue> : Broker, IEventConsumer<TQueue>
    {
        private readonly AsyncEventingBasicConsumer _consumer;

        public EventConsumer(ConnectionFactory factory)
            : base(factory)
        {
            _consumer = new AsyncEventingBasicConsumer(_channel);
        }

        public async void AddListener(AsyncEventHandler<BasicDeliverEventArgs> handler)
        {
            _consumer.ReceivedAsync += handler;

            await ConsumeAsync<TQueue>(_consumer);
        }
    }
}
