using BasketService.Application.UseCases.CommandHandlers.Baskets;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using Ecommerce.Brokers.Abstractions;
using Moq;

namespace BasketService.Tests.UnitTests.CommandHandlers
{
    public class AddItemToBasketCommandHandlerTest
    {
        [Fact]
        public async Task AddItemToBasket_ShouldReturnUserId()
        {
            // Arrange
            var repository = new Mock<IBasketRepository>();
            var producer = Mock.Of<IEventProducer>();

            var handler = new AddItemToBasketCommandHandler(repository.Object, producer);

            var request = new AddItemToBasketCommand(Guid.NewGuid(), Guid.NewGuid(), 3);

            var existingBasket = new Basket { UserId = request.UserId };

            repository.Setup(r => r.GetByUserIdAsync(request.UserId)).ReturnsAsync(existingBasket);
            repository.Setup(r => r.UpdateAsync(existingBasket)).ReturnsAsync(existingBasket);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(request.UserId, result);
            Assert.Equal(request.ProductId, existingBasket.Items[0].ProductId);
            repository.Verify(r => r.UpdateAsync(existingBasket), Times.Once);
        }
    }
}
