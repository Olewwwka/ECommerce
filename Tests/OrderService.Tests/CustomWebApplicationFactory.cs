using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Abstractions.Services;
using OrderService.Domain.Options;
using OrderService.Infrastructure.Services;
using RabbitMQ.Client;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace OrderService.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("test")
            .WithUsername("admin")
            .WithPassword("admin")
            .Build();

        private readonly RabbitMqContainer _rabbitContainer = new RabbitMqBuilder()
            .WithImage("rabbitmq:3-management")
            .WithPortBinding(5672, true)
            .WithUsername("guest")
            .WithPassword("guest")
            .Build();

        public HttpClient _httpClient { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            await _rabbitContainer.StartAsync();
            _httpClient = CreateClient();
        }

        public override async ValueTask DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
            await _rabbitContainer.DisposeAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Configure<DbOptions>(options =>
                {
                    options.OrderService = _dbContainer.GetConnectionString();
                });

                services.AddTransient<IDbInitializer, DbInitializer>();

                using var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();

                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitializer.InitializeDbAsync().GetAwaiter().GetResult();

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


            });
        }
    }

}
