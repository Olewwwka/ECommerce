using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using IdentityService.BLL.Exceptions;

namespace IdentityService.BLL.Services
{
    public class PasswordResetService : IPasswordResetService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IEmailService _emailService;
        private readonly IResetTokenRepository _resetTokenRepository;
        private readonly IPasswordHasher _passwordHasher;

        public PasswordResetService(IUsersRepository usersRepository, 
            IEmailService emailService,
            IResetTokenRepository resetTokenRepository,
            IPasswordHasher passwordHasher)
        {
            _usersRepository = usersRepository;
            _emailService = emailService;
            _resetTokenRepository = resetTokenRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> ResetPasswordAsync(string email, string token, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(email, cancellationToken);

            if(existingUser is null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var existingToken =  await _resetTokenRepository.GetResetTokenByTokenAsync(token, cancellationToken);

            var isTokenValid = ValidateResetToken(existingUser, existingToken, token);

            var paswordHash = _passwordHasher.HashPassword(password);

            existingUser.PasswordHash = paswordHash;
            await _usersRepository.UpdateAsync(existingUser, cancellationToken);

            existingToken.IsUsed = true;
            await _resetTokenRepository.UpdateAsync(existingToken, cancellationToken);

            return true;
        }

        public async Task SendResetEmailMessageAsync(string email, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(email, cancellationToken);

            if(existingUser is null)
            {
                throw new UserNotFoundException("User not found!");
            }

            var token = GenerateResetToken();

            var tokenEntity = new ResetTokenEntity
            {
                UserId = existingUser.Id,
                Token = token,
                ExspiresAt = DateTime.UtcNow.AddMinutes(30),
                IsUsed = false
            };

            await _resetTokenRepository.AddAsync(tokenEntity, cancellationToken);

            await _emailService.SendMessageAsync(email, token, cancellationToken);
        }

        private string GenerateResetToken()
        {
            return Guid.NewGuid().ToString("N");
        }

        private bool ValidateResetToken(UserEntity user, ResetTokenEntity? resetToken, string token)
        {
            return resetToken!= null && user.Id == resetToken.Id && !resetToken.IsUsed && resetToken.IsExpired && resetToken.Token == token;
        }

    }
}
