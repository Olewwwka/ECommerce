using AutoMapper;
using Ecommerce.Brokers.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.CommandHandlers;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Tests.UnitTests.CommandHandlers
{
    public class CreateOrderCommandHandlerTest
    {
        [Fact]
        public async Task ShouldCreateOrder_WhenUserDontHavePendingOrders()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IOrderReposotory>();
            var producer = Mock.Of<IEventProducer>();
            var logger = Mock.Of<ILogger<CreateOrderCommandHandler>>();

            var handler = new CreateOrderCommandHandler(mapper.Object, repository.Object, producer, logger);

            var dto = new CreateOrderDto
            {
                Items = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 10, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 20, Count = 1 },
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 30, Count = 1 }
                }
            };

            var command = new CreateOrderCommand(Guid.NewGuid(), dto);

            var orderEntity = new Order
            {
                UserId = command.UserId,
                Items = dto.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Count = item.Count
                }).ToList(),
            };

            mapper.Setup(m => m.Map<Order>(command)).Returns(orderEntity);

            repository.Setup(r => r.GetByUserId(orderEntity.UserId)).ReturnsAsync(new List<Order>());

            repository.Setup(r => r.CreateAsync(orderEntity)).ReturnsAsync(orderEntity.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(orderEntity.Id, result);
            Assert.Equal(60, orderEntity.TotalPrice);
        }

        [Fact]
        public async Task ShouldThrowException_WhenUserHasPendingOrder()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IOrderReposotory>();
            var producer = Mock.Of<IEventProducer>();
            var logger = Mock.Of<ILogger<CreateOrderCommandHandler>>();

            var handler = new CreateOrderCommandHandler(mapper.Object, repository.Object, producer, logger);

            var userId = Guid.NewGuid();

            var dto = new CreateOrderDto
            {
                Items = new List<CreateOrderItemDto>
                {
                    new CreateOrderItemDto { ProductId = Guid.NewGuid(), Price = 10, Count = 1 }
                }
            };

            var command = new CreateOrderCommand(userId, dto);

            var pendingOrder = new Order { UserId = userId, Status = OrderStatus.Pending };

            repository.Setup(r => r.GetByUserId(userId)).ReturnsAsync(new List<Order> { pendingOrder });

            // Act 
            var exception = await Assert.ThrowsAsync<UserHasUnpaidOrderException>(
                () => handler.Handle(command, CancellationToken.None)
            );


            // Assert
            repository.Verify(r => r.CreateAsync(It.IsAny<Order>()), Times.Never);
        }

    }
}
