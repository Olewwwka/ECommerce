using Newtonsoft.Json;
using OrderService.Tests;

namespace BasketService.Tests.IntegrationTests
{
    public class DeleteBasketTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public DeleteBasketTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteBasket_ShouldRemoveBasket()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            await _client.PostAsync($"/baskets/{userId}/items?productId={productId}&quantity=1", null);

            var deleteResponse = await _client.DeleteAsync($"/baskets/{userId}");
            var deletedBasketUserId = JsonConvert.DeserializeObject<Guid>(await deleteResponse.Content.ReadAsStringAsync());

            Assert.Equal(userId, deletedBasketUserId);
        }
    }
}
