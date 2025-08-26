using BasketService.Application.UseCases.CommandHandlers.Baskets;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using BasketService.Domain.Exceptions;
using Moq;

namespace BasketService.Tests.UnitTests.CommandHandlers
{
    public class DeleteBasketCommandHandlerTest
    {
        [Fact]
        public async Task ShouldDeleteBasket_WhenBasketExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var repository = new Mock<IBasketRepository>();

            var handler = new DeleteBasketCommandHandler(repository.Object);

            repository.Setup(repo => repo.GetByUserIdAsync(userId)).ReturnsAsync(new Basket { UserId = userId });
            repository.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(new DeleteBasketCommand(userId), CancellationToken.None);

            // Assert
            Assert.Equal(userId, result);
            repository.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowNotFoundException_WhenBasketDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var repository = new Mock<IBasketRepository>();

            var handler = new DeleteBasketCommandHandler(repository.Object);

            repository.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync((Basket)null);

            var command = new DeleteBasketCommand(userId);

            // Act 
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // Assert
            repository.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
