using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Services;
using IdentityService.DAL.Abstractions;
using Moq;

namespace IdentityService.Tests.UnitTests.AuthServiceTests
{
    [Collection("AuthServiceTests")]
    public class RegisterTest
    {
        [Fact]
        public async Task ShouldReturnPairOfTokens()
        {
            //Arrange
            var usersRepository = new Mock<IUsersRepository>();
            var passwordHasher = new Mock<IPasswordHasher>();
            var tokenService = new Mock<IAccessTokenService>();
            var refreshTokenService = new Mock<IRefreshTokenService>();
            var rolesRepository = Mock.Of<IRolesRepository>();
            var mapper = new Mock<IMapper>();

            var service = new AuthService(usersRepository.Object, passwordHasher.Object, mapper.Object, tokenService.Object, refreshTokenService.Object, rolesRepository);
        }
    }
}
