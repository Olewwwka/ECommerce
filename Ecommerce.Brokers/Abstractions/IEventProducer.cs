namespace Ecommerce.Brokers.Abstractions
{
    public interface IEventProducer
    {
        Task SendAsync<T>(T message, CancellationToken cancellationToken) where T : class;  
    }
}
