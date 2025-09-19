using Ecommerce.Brokers.Events;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Abstractions.Handlers;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL.EventHandlers
{
    public class PutUpUserBallanceEventHandler : IPutUpUserBallanceHandler
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<PutUpUserBallanceEventHandler> _logger;

        public PutUpUserBallanceEventHandler(IUsersRepository usersRepository, ILogger<PutUpUserBallanceEventHandler> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }
        public async Task HandleAsync(PutUpBallanceEvent ballanceEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PutUpBallanceEvent for User ID: {UserId}, Amount: {Amount}", ballanceEvent.UserId, ballanceEvent.Amount);

            var exisingUser = await _usersRepository.GetByIdAsync(ballanceEvent.UserId, cancellationToken);

            if(exisingUser is null)
            {
                throw new UserNotFoundException($"User with id {ballanceEvent.UserId} not found =(");
            }

            exisingUser.Ballance += ballanceEvent.Amount;
            await _usersRepository.UpdateAsync(exisingUser, cancellationToken);

            _logger.LogInformation("Updated User ID: {UserId} with new Ballance: {Ballance}", exisingUser.Id, exisingUser.Ballance);
        }
    }
}
