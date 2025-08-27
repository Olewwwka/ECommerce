using AutoMapper;
using Moq;
using OrderService.Application.DTO;
using OrderService.Application.UseCases.Queries;
using OrderService.Application.UseCases.QueryHandlers;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;

namespace OrderService.Tests.UnitTests.QueryHandlers
{
    public class GetOrderByIdQueryHandlerTest
    {
        [Fact]
        public async Task ShouldReturnOrder_WhexExists()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IOrderReposotory>();

            var handler = new GetOrderByIdQueryHandler(mapper.Object, repository.Object);

            var orderEntity = new Order { Id = Guid.NewGuid() };
            var orderDto = new OrderDto { Id = orderEntity.Id };

            mapper.Setup(m => m.Map<OrderDto>(orderEntity)).Returns(orderDto);

            repository.Setup(r => r.GetByIdAsync(orderEntity.Id)).ReturnsAsync(orderEntity);

            var query = new GetOrderByIdQuery(orderEntity.Id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Asserts
            Assert.Equal(orderDto.Id, result.Id);

            repository.Verify(r => r.GetByIdAsync(orderDto.Id), Times.Once);
        }
    }
}
