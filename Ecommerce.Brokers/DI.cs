using Ecommerce.Brokers.Abstractions;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Ecommerce.Brokers
{
    public static class DI
    {
        public static void AddBrokers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(new ConnectionFactory()
            {
                HostName = configuration["Broker:Host"],
                Port = int.Parse(configuration["Broker:Port"])
            });

            services.AddSingleton<IEventProducer, EventProducer>();

            services.AddSingleton(typeof(IEventConsumer<>), typeof(EventConsumer<>));
        }
    }
}
