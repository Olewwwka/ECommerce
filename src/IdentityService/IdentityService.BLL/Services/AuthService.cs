using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher, 
            IMapper mapper, 
            IAccessTokenService tokenService, 
            IRefreshTokenService refreshTokenService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (existingUser == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var isPasswordCorrect = _passwordHasher.VerifyPassword(request.Password, existingUser.PasswordHash);

            if(!isPasswordCorrect)
            {
                throw new IncorrectPasswordException("Incorrect password!!");
            }

            var accessToken = _tokenService.GenerateAccessToken(existingUser.Id, existingUser.Name, existingUser.Email, existingUser.Role);

            var refreshToken = _refreshTokenService.GenerateRefreshToken(existingUser.Id);

            await _refreshTokenService.SaveRefreshTokenAsync(refreshToken);

            return new AuthResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if(existingUser != null)
            {
                throw new UserAlreadyExistsException($"User with email {request.Email} already exists!");
            }

            var userEntity = _mapper.Map<UserEntity>(request);

            await _usersRepository.CreateUserAsync(userEntity, cancellationToken);

            var accessToken = _tokenService.GenerateAccessToken(userEntity.Id, userEntity.Name, userEntity.Email, userEntity.Role);

            var refreshToken = _refreshTokenService.GenerateRefreshToken(userEntity.Id);

            await _refreshTokenService.SaveRefreshTokenAsync(refreshToken);

            return new AuthResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<AuthResponse> RefreshAsync(string accessToken, string refreshToken, CancellationToken cancellationToken)
        {
            var existingRefreshToken = await _refreshTokenService.GetRefreshTokenAsync(refreshToken);

            if(existingRefreshToken == null)
            {
                throw new RefreshTokenNotFoundException("Refresh token not found!");
            }

            if(existingRefreshToken.IsExpired)
            {
                throw new RefreshTokenExpiredException("Refresh token expired!");
            }

            var existingUser = await _usersRepository.GetUserByIdAsync(existingRefreshToken.UserId, cancellationToken);

            if(existingUser == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(existingUser.Id, existingUser.Name, existingUser.Email, existingUser.Role);

            var newRefreshToken = _refreshTokenService.GenerateRefreshToken(existingUser.Id);

            await _refreshTokenService.DeleteRefreshTokenAsync(refreshToken);

            await _refreshTokenService.SaveRefreshTokenAsync(newRefreshToken);

            return new AuthResponse() { AccessToken = accessToken, RefreshToken=newRefreshToken };
        }
    }
}
