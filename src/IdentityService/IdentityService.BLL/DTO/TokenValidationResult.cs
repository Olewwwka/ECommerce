using System.Security.Claims;

namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Result of validating of token.
    /// </summary>
    public class TokenValidationResult
    {
        public bool IsValid { get; set; }
        public ClaimsPrincipal? Claims { get; set; }
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
