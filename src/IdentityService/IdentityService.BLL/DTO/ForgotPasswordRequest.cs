namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Request object for sending password reset email.
    /// </summary>
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }
}
