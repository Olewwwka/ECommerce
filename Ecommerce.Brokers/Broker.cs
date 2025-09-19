using RabbitMQ.Client;

namespace Ecommerce.Brokers
{
    public class Broker
    {
        private readonly IConnection _connection;
        protected readonly IChannel _channel;
        public Broker(ConnectionFactory factory)
        {
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
        }

        protected async Task CreateExchangeAsync<TQueue>(CancellationToken cancellationToken = default)
        {
            await _channel.ExchangeDeclareAsync(
                exchange: typeof(TQueue).Name,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);
        }

        protected async Task<string> CreateQueueAsync<TQueue>(CancellationToken cancellationToken = default)
        {
            var queueName = $"{typeof(TQueue).Name}_{Guid.NewGuid()}";
            
            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken);

            await _channel.QueueBindAsync(
                queue: queueName,
                exchange: typeof(TQueue).Name,
                routingKey: "",
                cancellationToken: cancellationToken);

            return queueName;
        }

        protected async Task PublishAsync<TQueue>(byte[] messageBody, 
            CancellationToken cancellationToken = default)
        {
            var properties = new BasicProperties()
            {
                ContentType = "application/json",
                Persistent = true
            };

            await _channel.BasicPublishAsync(
                exchange: typeof(TQueue).Name, 
                routingKey: "", 
                mandatory: true,
                basicProperties: properties,
                body: messageBody,
                cancellationToken: cancellationToken);
        }

        protected async Task<string> ConsumeAsync<TQueue>(IAsyncBasicConsumer consumer,
            CancellationToken cancellationToken = default)
        {
            await CreateExchangeAsync<TQueue>(cancellationToken);

            var queueName = await CreateQueueAsync<TQueue>(cancellationToken);
            
            await _channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: true,
                consumer: consumer,
                noLocal: false,
                exclusive: false,
                consumerTag: Guid.NewGuid().ToString(),
                arguments: null,
                cancellationToken: cancellationToken);

            return queueName;
        }
    }
}
