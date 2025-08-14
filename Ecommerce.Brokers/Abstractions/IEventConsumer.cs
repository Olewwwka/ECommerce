using RabbitMQ.Client.Events;

namespace Ecommerce.Brokers.Abstractions
{
    public interface IEventConsumer<TQueue>
    {
        void AddListener(AsyncEventHandler<BasicDeliverEventArgs> handler);
    }
}
