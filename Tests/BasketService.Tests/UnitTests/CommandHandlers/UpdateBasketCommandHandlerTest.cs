using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Application.UseCases.CommandHandlers.Baskets;
using BasketService.Application.UseCases.Commands.Baskets;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using Moq;

namespace BasketService.Tests.UnitTests.CommandHandlers
{
    public class UpdateBasketCommandHandlerTest
    {
        [Fact]
        public async Task ShouldUpdateBasket_WhenBasketExists()
        {
            // Arrange
            var repository = new Mock<IBasketRepository>();
            var mapper = new Mock<IMapper>();

            var handler = new UpdateBasketCommandHandler(repository.Object, mapper.Object);

            var userId = Guid.NewGuid();
            var basketEntity = new Basket { UserId = userId, Items = new List<BasketItem>() };
            var basketDto = new BasketDto { UserId = userId, Items = new List<BasketItemDto>() };

            var command = new UpdateBasketCommand(userId, basketDto);

            repository.Setup(r => r.GetByUserIdAsync(userId)).ReturnsAsync(basketEntity);
            repository.Setup(r => r.UpdateAsync(It.IsAny<Basket>())).ReturnsAsync(basketEntity);

            mapper.Setup(m => m.Map<Basket>(basketDto)).Returns(basketEntity);
            mapper.Setup(m => m.Map<BasketDto>(basketEntity)).Returns(basketDto);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(userId, result.UserId);
            repository.Verify(r => r.UpdateAsync(It.IsAny<Basket>()), Times.Once);
        }
    }
}
