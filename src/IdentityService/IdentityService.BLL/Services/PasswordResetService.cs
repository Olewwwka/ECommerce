using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using IdentityService.BLL.Exceptions;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic to reset forgotten users password
    /// Contains such methods, as send email message to reset password 
    /// and reset password with help of recieved email token
    /// </summary>
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
        /// <summary>
        /// Method to reset forgotten users password
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="token">Unique token to reset password</param>
        /// <param name="password">Password of user</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        /// <returns>Boolean result of operation(success or not)</returns>
        /// <exception cref="UserNotFoundException">If user not found in database</exception>
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
