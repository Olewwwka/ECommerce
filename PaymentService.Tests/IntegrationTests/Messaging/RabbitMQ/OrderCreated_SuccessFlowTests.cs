using System.Text;
using System.Text.Json;
using Ecommerce.Brokers.Consumers;
using Ecommerce.Brokers.Events;
using Ecommerce.Brokers.Producers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PaymentService.Application.Abstractions;
using PaymentService.Application.DTO;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using RabbitMQ.Client;

namespace PaymentService.Tests.IntegrationTests.Messaging.RabbitMQ
{
    public class OrderCreated_SuccessFlowTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _baseFactory;

        public OrderCreated_SuccessFlowTests(CustomWebApplicationFactory factory)
        {
            _baseFactory = factory;
        }

        [Fact(Timeout = 30000)]
        public async Task OrderCreatedEvent_Should_Publish_PaymentSucceedEvent_And_SaveReceipt()
        {
            var factory = _baseFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var existing = services.FirstOrDefault(d => d.ServiceType == typeof(IWithdrawBallanceService));
                    if (existing != null) services.Remove(existing);
                    services.AddSingleton<IWithdrawBallanceService>(new StubWithdraw(new WithdrawResult(true, null)));
                });
            });

            var sp = factory.Services;
            var db = _baseFactory.Database;
            var connectionFactory = sp.GetRequiredService<ConnectionFactory>();

            var taskSource = new TaskCompletionSource<PaymentSucceedEvent>(TaskCreationOptions.RunContinuationsAsynchronously);

            var consumer = new EventConsumer<PaymentSucceedEvent>(connectionFactory);
            consumer.AddListener(async (obj, args) =>
            {
                var json = Encoding.UTF8.GetString(args.Body.ToArray());
                var @event = JsonSerializer.Deserialize<PaymentSucceedEvent>(json);
                if (@event != null) taskSource.TrySetResult(@event);
                await Task.CompletedTask;
            });

            await Task.Delay(1000);

            var producer = new EventProducer(connectionFactory);
            var orderId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var amount = 100m;

            await producer.SendAsync(new OrderCreatedEvent(orderId, userId, amount), CancellationToken.None);

            var completed = await Task.WhenAny(taskSource.Task, Task.Delay(TimeSpan.FromSeconds(15)));

            var succeed = await taskSource.Task;
            Assert.Equal(orderId, succeed.OrderId);
            Assert.Equal(userId, succeed.UserId);
            Assert.Equal(amount, succeed.Amount);

            var receipts = db.GetCollection<Receipt>("receipts");

            var receipt = await receipts.Find(r => r.OrderId == orderId).FirstOrDefaultAsync();
            Assert.NotNull(receipt);
            Assert.Equal(amount, receipt.TotalCost);
            Assert.Equal(PaymentStatus.Success, receipt.Status);
        }

        private sealed class StubWithdraw : IWithdrawBallanceService
        {
            private readonly WithdrawResult _result;
            public StubWithdraw(WithdrawResult result) { _result = result; }
            public Task<WithdrawResult> TryWithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
                => Task.FromResult(_result);
        }
    }
}


