namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Response object for a withdraw money.
    /// </summary>
    public record WithdrawResponse(bool IsSuccess, string? Reason)
    {
    }
}
