namespace IdentityService.BLL.DTO
{
    /// <summary>
    /// Request object for user registration.
    /// </summary>
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
