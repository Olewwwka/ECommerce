using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using Moq;

namespace IdentityService.Tests.UnitTests.AuthServiceTests
{
    [Collection("AuthServiceTests")]
    public class LoginTest
    {
        [Fact]
        public async Task LoginAsync_ShouldReturnTokens_WhenCredentialsAreCorrect()
        {
            //Arrange
            var usersRepository = new Mock<IUsersRepository>();
            var passwordHasher = new Mock<IPasswordHasher>();
            var tokenService = new Mock<IAccessTokenService>();
            var refreshTokenService = new Mock<IRefreshTokenService>();
            var rolesRepository = Mock.Of<IRolesRepository>();

            var mapper = new Mock<IMapper>();

            var service = new AuthService(usersRepository.Object, passwordHasher.Object, mapper.Object,
                tokenService.Object, refreshTokenService.Object, rolesRepository);

            var userId = Guid.NewGuid();
            var email = "test@mail.com";

            var user = new UserEntity
            {
                Id = userId,
                Email = "test@mail.com",
                Name = "Test",
                PasswordHash = "passwordHash",
                UserRoles = new List<UserRoleEntity>
                {
                    new() { Role = new RoleEntity { Name = "User" } }
                }
            };

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(123)
            };

            usersRepository.Setup(r => r.GetUserByEmailAsync(email, default)).ReturnsAsync(user);
            passwordHasher.Setup(h => h.VerifyPassword("password", "passwordHash")).Returns(true);
            tokenService.Setup(t => t.GenerateAccessToken(userId, "Test", email, It.IsAny<IEnumerable<string>>())).Returns("access-token");
            refreshTokenService.Setup(r => r.GenerateRefreshToken(userId)).Returns(refreshToken);

            // Act
            var result = await service.LoginAsync(new LoginRequest { Email = "test@mail.com", Password = "password" });

            // Assert
            Assert.Equal("access-token", result.AccessToken);
            Assert.Equal(refreshToken, result.RefreshToken);
            refreshTokenService.Verify(r => r.SaveRefreshTokenAsync(refreshToken), Times.Once);
        }
    }
}
