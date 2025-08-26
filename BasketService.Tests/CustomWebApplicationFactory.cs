using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Net;
using Testcontainers.RabbitMq;
using Testcontainers.Redis;

namespace OrderService.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:7-alpine")
            .WithPortBinding(6379, true)
            .Build();

        private readonly RabbitMqContainer _rabbitContainer = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithPortBinding(5672, true)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();//fixture

        public HttpClient _httpClient { get; private set; } = null!;
        public CookieContainer _cookieContainer { get; private set; } = null!;
        public IConnectionMultiplexer RedisConnection { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            await _redisContainer.StartAsync();
            await _rabbitContainer.StartAsync();

            RedisConnection = ConnectionMultiplexer.Connect(_redisContainer.GetConnectionString());
        }

        public async Task DisposeAsync()
        {
            if (RedisConnection != null)
            {
                await RedisConnection.CloseAsync();
                RedisConnection.Dispose();
            }

            await _redisContainer.DisposeAsync();
            await _rabbitContainer.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IConnectionMultiplexer>(sp => RedisConnection);

                services.AddSingleton(sp =>
                {
                    return new ConnectionFactory
                    {
                        HostName = _rabbitContainer.Hostname,
                        Port = _rabbitContainer.GetMappedPublicPort(5672),
                        UserName = "guest",
                        Password = "guest"
                    };
                });

                services.AddControllers().AddNewtonsoftJson();
            });

        }
    }
}