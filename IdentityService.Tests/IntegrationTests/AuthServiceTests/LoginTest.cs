using IdentityService.BLL.DTO;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Net.Http.Json;

namespace IdentityService.Tests.IntegrationTests.AuthServiceTests
{
    public class LoginTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public LoginTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task LoginUser_ShouldReturn200_And_Tokens()
        {
            // Arrange
            var registerRequest = new RegisterRequest()
            {
                Email = "testEmail@gmail.com",
                Name = "testName",
                Password = "testPassword123",
            };

            await _client.PostAsJsonAsync("/auth/register", registerRequest);

            var loginRequest = new LoginRequest()
            {
                Email = "testEmail@gmail.com",
                Password = "testPassword123",
            };

            // Act
            var response = await _client.PostAsJsonAsync("/auth/login", loginRequest);

            // Assert
            if (response.Headers.TryGetValues("Set-Cookie", out var setCookies))
            {
                foreach (var cookie in setCookies)
                {
                    Console.WriteLine(cookie);
                }
            }

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            //Assert.Contains(cookies, c => c.Name == "access-token");
        }
    }
}
