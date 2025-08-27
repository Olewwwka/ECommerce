using BasketService.Application.DTO;
using Newtonsoft.Json;
using OrderService.Tests;

namespace BasketService.Tests.IntegrationTests
{
    public class AddItemToBasketTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public AddItemToBasketTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddItemToBasket_ShouldCreateBasketAndAddItem()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var quantity = 23;

            var response = await _client.PostAsync($"/baskets/{userId}/items?productId={productId}&quantity={quantity}", null);

            var returnedUserId = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());

            var getResponse = await _client.GetAsync($"/baskets/{userId}");

            var basketId = JsonConvert.DeserializeObject<BasketDto>(await getResponse.Content.ReadAsStringAsync());

            Assert.NotNull(basketId);
        }

    }
}
