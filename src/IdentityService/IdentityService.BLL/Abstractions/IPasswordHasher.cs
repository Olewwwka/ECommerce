namespace IdentityService.BLL.Abstractions
{
    /// <summary>
    /// Service for hashing and verifying passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Hash password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Hashed password.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verified password and hashed password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <param name="passwordHash">Hashed password.</param>
        /// <returns>result of verifying</returns>
        bool VerifyPassword(string password, string passwordHash);
    }
}
