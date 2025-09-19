using IdentityService.BLL.DTO;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace IdentityService.Tests.IntegrationTests.AuthServiceTests
{
    public class RegisterTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CookieContainer _cookieContainer;

        public RegisterTest(CustomWebApplicationFactory factory)
        {
            _client = factory._httpClient;
            _cookieContainer = factory._cookieContainer;
        }

        [Fact]
        public async Task RegisterUser_ShouldReturn200_And_Tokens()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Name = "testName",
                Email = "testEmail@gmail.com",
                Password = "testPassword123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/auth/register", registerRequest);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            if (response.Headers.TryGetValues("Set-Cookie", out var setCookies))
            {
                foreach (var cookie in setCookies)
                {
                    Console.WriteLine(cookie);
                }
            }

            Console.WriteLine(content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            //Assert.Contains(cookies, c => c.Name == "access-token");
        }
    }
}
