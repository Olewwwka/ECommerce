using AutoMapper;
using BasketService.Application.DTO;
using BasketService.Application.UseCases.Queries.Baskets;
using BasketService.Application.UseCases.QueryHandlers;
using BasketService.Domain.Abstractions;
using BasketService.Domain.Entities;
using Moq;

namespace BasketService.Tests.UnitTests.QueryHandlers
{
    public class GetBasketByUserIdQueryHandlerTest
    {
        [Fact]
        public async Task GetBasketByUserId_ShouldReturnBasket()
        {
            // Arrange
            var mapper = new Mock<IMapper>();
            var repository = new Mock<IBasketRepository>();

            var handler = new GetBasketByUserIdQueryHandler(repository.Object, mapper.Object);

            var basket = new Basket { UserId = Guid.NewGuid() };
            var basketDto = new BasketDto { UserId = basket.UserId };

            mapper.Setup(m => m.Map<BasketDto>(basket)).Returns(basketDto);

            repository.Setup(r => r.GetByUserIdAsync(basket.UserId)).ReturnsAsync(basket);

            // Act
            var result = await handler.Handle(new GetBasketByUserIdQuery(basket.UserId), CancellationToken.None);

            //Assert
            Assert.Equal(basketDto.UserId, result.UserId);
            repository.Verify(r => r.GetByUserIdAsync(basket.UserId), Times.Once);
        }
    }
}
