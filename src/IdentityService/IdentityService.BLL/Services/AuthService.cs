using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Constants;
using IdentityService.DAL.Entities;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Service that contains logic of authorization
    /// Contains such methods as login user, register user and refresh access token of user
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRolesRepository _rolesRepository;

        public AuthService(IUsersRepository usersRepository, 
            IPasswordHasher passwordHasher, 
            IMapper mapper, 
            IAccessTokenService tokenService, 
            IRefreshTokenService refreshTokenService,
            IRolesRepository rolesRepository)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _rolesRepository = rolesRepository;
        }
        /// <summary>
        /// Logging user in system
        /// </summary>
        /// <param name="request">Request that contains email and password of user</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        /// <returns>AuthResponse, that contains information about user and pair of access and refresh tokens</returns>
        /// <exception cref="UserNotFoundException">If user not found in database</exception>
        /// <exception cref="IncorrectPasswordException">If user sent incorrect password</exception>
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

            var userRoles = existingUser.UserRoles.Select(ur => ur.Role.Name);

            var accessToken = _tokenService.GenerateAccessToken(existingUser.Id, existingUser.Name, existingUser.Email, userRoles);

            var refreshToken = _refreshTokenService.GenerateRefreshToken(existingUser.Id);

            await _refreshTokenService.SaveRefreshTokenAsync(refreshToken);

            return new AuthResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        /// <summary>
        /// Register user in sustem
        /// </summary>
        /// <param name="request">Request that contains name, email and password of user</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        /// <returns>AuthResponse, that contains information about user and pair of access and refresh tokens</returns>
        /// <exception cref="UserAlreadyExistsException">If user already exists in database</exception>
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if(existingUser != null)
            {
                throw new UserAlreadyExistsException($"User with email {request.Email} already exists!");
            }

            var userEntity = _mapper.Map<UserEntity>(request);

            var defaultUserRole = await _rolesRepository.GetRoleByNameAsync(UserRoles.User, cancellationToken);

            userEntity.UserRoles = new List<UserRoleEntity>
            {
                new UserRoleEntity
                {
                    User = userEntity,
                    RoleId = defaultUserRole.Id,
                    Role = defaultUserRole
                }
            };

            await _usersRepository.AddAsync(userEntity, cancellationToken);

            var userRoles = userEntity.UserRoles.Select(ur => ur.Role.Name).ToList();

            var accessToken = _tokenService.GenerateAccessToken(userEntity.Id, userEntity.Name, userEntity.Email, userRoles);

            var refreshToken = _refreshTokenService.GenerateRefreshToken(userEntity.Id);

            await _refreshTokenService.SaveRefreshTokenAsync(refreshToken);

            return new AuthResponse() { AccessToken = accessToken, RefreshToken = refreshToken };
        }
        /// <summary>
        /// Refresh access token of user
        /// </summary>
        /// <param name="accessToken">Access token, received as a result of authorization</param>
        /// <param name="refreshToken">Refresh token, received as a result of authorization</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        /// <returns>AuthResponse, that contains information about user and pair of access and refresh tokens</returns>
        /// <exception cref="RefreshTokenNotFoundException">If refresh token not found in database</exception>
        /// <exception cref="RefreshTokenExpiredException">If refresh token expired</exception>
        /// <exception cref="UserNotFoundException">If user not found in database</exception>
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

            var existingUser = await _usersRepository.GetByIdAsync(existingRefreshToken.UserId, cancellationToken);

            if(existingUser == null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var userRoles = existingUser.UserRoles.Select(ur => ur.Role.Name);

            var newAccessToken = _tokenService.GenerateAccessToken(existingUser.Id, existingUser.Name, existingUser.Email, userRoles);

            var newRefreshToken = _refreshTokenService.GenerateRefreshToken(existingUser.Id);

            await _refreshTokenService.DeleteRefreshTokenAsync(refreshToken);

            await _refreshTokenService.SaveRefreshTokenAsync(newRefreshToken);

            return new AuthResponse() { AccessToken = newAccessToken, RefreshToken=newRefreshToken };
        }
    }
}
