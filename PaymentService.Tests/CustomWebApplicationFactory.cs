using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PaymentService.Domain.Abstractions.Services;
using PaymentService.Infrastructure.Data;
using RabbitMQ.Client;
using Testcontainers.MongoDb;
using Testcontainers.RabbitMq;

namespace PaymentService.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:6.0")
            .WithUsername("guest")
            .WithPassword("guest")
            .WithPortBinding(27017, true)
            .Build();

        private readonly RabbitMqContainer _rabbitContainer = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithPortBinding(5672, true)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();

        public HttpClient _httpClient { get; private set; } = null!;
        public IMongoDatabase Database { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            
            await _mongoContainer.StartAsync();
            await _rabbitContainer.StartAsync();

            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var mongoClient = new MongoClient(_mongoContainer.GetConnectionString());
            Database = mongoClient.GetDatabase("test");

            var initializer = new MongoInitializer(Database);
            await initializer.InitializeAsync();

            _httpClient = CreateClient();
        }

        public async Task DisposeAsync()
        {
            await _mongoContainer.DisposeAsync();
            await _rabbitContainer.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IMongoDatabase>(sp => Database);
                services.AddTransient<IMongoInitializer, MongoInitializer>();

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
