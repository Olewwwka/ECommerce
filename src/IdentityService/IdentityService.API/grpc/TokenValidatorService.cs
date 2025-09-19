using Grpc.Core;
using IdentityService.BLL.Abstractions;
using IdentityService.Grpc;

namespace IdentityService.API.grpc
{
    public class TokenValidatorService : TokenValidator.TokenValidatorBase
    {
        private readonly IAccessTokenService _tokenService;
        public TokenValidatorService(IAccessTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public override Task<ValidateTokenResponse> ValidateToken(ValidateTokenRequest request, ServerCallContext context)
        {

            var principal = _tokenService.ValidateAccessToken(request.AccessToken);

            var response = new ValidateTokenResponse();

            if (principal is not null)
            {
                response.IsValid = true;

                foreach (var claim in principal.Claims)
                {
                    response.Claims.Add(new Claim
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    });
                }

                return Task.FromResult(response);
            }

            response.IsValid = false;

            return Task.FromResult(response);
        }

    }
}
