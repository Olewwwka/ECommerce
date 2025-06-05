using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthService(IUsersRepository usersRepository, IPasswordHasher passwordHasher, IMapper mapper, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (existingUser == null)
            {
                throw new Exception();
            }

            var isPasswordCorrect = _passwordHasher.VerifyPassword(request.Password, existingUser.PasswordHash);

            if(!isPasswordCorrect)
            {
                throw new Exception();
            }

            var accessToken = _tokenService.GenerateAccessToken(existingUser);

            return new AuthResponse() { AccessToken = accessToken };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if(existingUser != null)
            {
                throw new Exception(); 
            }

            var userEntity = _mapper.Map<UserEntity>(request);

            try
            {
                await _usersRepository.CreateUserAsync(userEntity, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            var accessToken = _tokenService.GenerateAccessToken(userEntity);

            return new AuthResponse() {AccessToken = accessToken};
        }
    }
}
