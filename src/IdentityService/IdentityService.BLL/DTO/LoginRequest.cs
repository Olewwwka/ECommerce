namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Request object for user login.
    /// </summary>
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
