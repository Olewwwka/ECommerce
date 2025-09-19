namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Request object for reset user password.
    /// </summary>
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password {  get; set; }
    }
}
