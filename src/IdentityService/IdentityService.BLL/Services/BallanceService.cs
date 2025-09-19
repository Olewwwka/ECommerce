using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;

namespace IdentityService.BLL.Services
{
    /// <summary>
    /// Serivce that contains logic to working with users ballance
    /// </summary>
    public class BallanceService : IBallanceService
    {
        private readonly IUsersRepository _usersRepository;
        public BallanceService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        /// <summary>
        /// Withdraw money from user ballance
        /// </summary>
        /// <param name="userId">Unique ID of user</param>
        /// <param name="amount">The amount to be debited</param>
        /// <param name="cancellationToken">A token that allows you to cancel an asynchronous operation</param>
        /// <returns>WithdrawResponse, that contains withdraw status and amount</returns>
        /// <exception cref="InsufficientFundsException">If there is not enough money in user ballance</exception>
        /// <exception cref="UserNotFoundException">If user not found in database</exception>
        public async Task<WithdrawResponse> WithdrawAsync(Guid userId, decimal amount, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetByIdAsync(userId, cancellationToken);

            if(existingUser is null)
            {
                throw new UserNotFoundException($"User with ID {userId} not found");
            }

            if (existingUser.Ballance < amount)
            {
                throw new InsufficientFundsException($"User with ID {userId} has insufficient funds");
            }

            existingUser.Ballance -= amount;

            await _usersRepository.UpdateAsync(existingUser, cancellationToken);


            var response = new WithdrawResponse(IsSuccess: true, Reason: $"Successfully withdrew {amount} from user with ID {userId}");

            return response;
        }
    }
}
