using AutoMapper;
using Moq;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Queries;
using OrderService.Application.UseCases.QueryHandlers;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;

namespace OrderService.Tests.UnitTests.QueryHandlers
{
    public class GetOrdersByUserIdQueryHandlerTest
    {
        [Fact]
        public async Task ShouldReturnAllOrders()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IOrderReposotory>();

            var handler = new GetOrdersByUserIdQueryHandler(mapper.Object, repository.Object);

            var userId = Guid.NewGuid();

            var ordersEntity = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), UserId = userId },
                new Order { Id = Guid.NewGuid(), UserId = userId }
            };

            var ordersDto = new List<OrderDto>
            {
                new OrderDto { Id = ordersEntity[0].Id, UserId = userId },
                new OrderDto { Id = ordersEntity[1].Id, UserId = userId }
            };

            repository.Setup(r => r.GetByUserId(userId)).ReturnsAsync(ordersEntity);
            mapper.Setup(m => m.Map<List<OrderDto>>(ordersEntity)).Returns(ordersDto);

            var query = new GetOrdersByUserIdQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.Equal(userId, r.UserId));

            repository.Verify(r => r.GetByUserId(userId), Times.Once);
            mapper.Verify(m => m.Map<List<OrderDto>>(ordersEntity), Times.Once);
        }
    }
}
