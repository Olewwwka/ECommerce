using IdentityService.DAL.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System.Net;
using System.Net.Http.Headers;
using Testcontainers.PostgreSql;
using Testcontainers.RabbitMq;

namespace IdentityService.Tests
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
        public CookieContainer _cookieContainer { get; private set; } = new();

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            await _rabbitContainer.StartAsync();

            var handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = _cookieContainer
            };

            _httpClient = CreateClient(new WebApplicationFactoryClientOptions
            {
                HandleCookies = true,
                BaseAddress = new Uri("http://localhost")
            });

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test");
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
            await _rabbitContainer.DisposeAsync();
            await base.DisposeAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<IdentityServiceDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<IdentityServiceDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                services.AddSingleton(sp =>
                    new ConnectionFactory
                    {
                        HostName = _rabbitContainer.Hostname,
                        Port = _rabbitContainer.GetMappedPublicPort(5672),
                        UserName = "guest",
                        Password = "guest"
                    });

                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                services.PostConfigure<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });

                services.AddAuthorization();
                using var spBuild = services.BuildServiceProvider();
                using var scope = spBuild.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();
                db.Database.Migrate();
            });
        }

    }
}
