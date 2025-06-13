using System.Security.Claims;

namespace IdentityService.BLL.DTO
{
    public class TokenValidationResult
    {
        public bool IsValid { get; set; }
        public ClaimsPrincipal? Claims { get; set; }
        public string? ErrorMessage { get; set; } = string.Empty;
    }
}
