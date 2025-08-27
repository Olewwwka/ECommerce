using BasketService.Application.DTO;
using Newtonsoft.Json;
using OrderService.Tests;

namespace BasketService.Tests.IntegrationTests
{
    public class GetBasketByUserIdTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetBasketByUserIdTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetByUserId_ShouldReturnBasket()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 123;

            await _client.PostAsync($"/baskets/{userId}/items?productId={productId}&quantity={quantity}", null);

            var getResponse = await _client.GetAsync($"/baskets/{userId}");
            var basket = JsonConvert.DeserializeObject<BasketDto>(await getResponse.Content.ReadAsStringAsync());


            Console.WriteLine($"Status: {getResponse.StatusCode}");
            Console.WriteLine($"Body: {basket}");


            Assert.Equal(userId, basket.UserId);
            Assert.Single(basket.Items);
            Assert.Equal(productId, basket.Items[0].ProductId);
            Assert.Equal(quantity, basket.Items[0].Quantity);
        }
    }
}
