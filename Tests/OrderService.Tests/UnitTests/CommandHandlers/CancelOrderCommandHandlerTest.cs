using Moq;
using OrderService.Application.UseCases.CommandHandlers;
using OrderService.Application.UseCases.Commands;
using OrderService.Domain.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using OrderService.Domain.Exceptions;

namespace OrderService.Tests.UnitTests.CommandHandlers
{
    public class CancelOrderCommandHandlerTest
    {
        [Fact]
        public async Task ShouldCancelOrder_WhenOrderExists()
        {
            // Arrange
            var repository = new Mock<IOrderReposotory>();
            var commandHandler = new CancelOrderCommandHandler(repository.Object);

            var order = new Order { Id = Guid.NewGuid(), Status = OrderStatus.Pending };

            repository.Setup(r => r.GetByIdAsync(order.Id)).ReturnsAsync(order);

            repository.Setup(r => r.UpdateStatusAsync(order.Id, It.IsAny<OrderStatus>())).ReturnsAsync(order.Id);

            var command = new CancelOrderCommand(order.Id);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(order.Id, result);
            repository.Verify(r => r.UpdateStatusAsync(order.Id, OrderStatus.Rejected), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowException_WhenOrderNotExist()
        {
            // Arrange
            var repository = new Mock<IOrderReposotory>();
            var commandHandler = new CancelOrderCommandHandler(repository.Object);

            var nonExistentOrderId = Guid.NewGuid();

            repository.Setup(r => r.GetByIdAsync(nonExistentOrderId)).ReturnsAsync((Order)null);

            var command = new CancelOrderCommand(nonExistentOrderId);

            // Act 
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => commandHandler.Handle(command, CancellationToken.None));

            // Assert
            repository.Verify(r => r.UpdateStatusAsync(It.IsAny<Guid>(), It.IsAny<OrderStatus>()), Times.Never);
        }
    }
}
