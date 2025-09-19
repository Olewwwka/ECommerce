using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.API.Controllers
{
    /// <summary>
    /// Controller with logic for working with users.
    /// Provides endpoints to register, login, refresh access tokens, and reset passwords.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IAuthService authService, IPasswordResetService passwordResetService)
        {
            _authService = authService;
            _passwordResetService = passwordResetService;
        }

        /// <summary>
        /// Register a new user and save it in  database.
        /// </summary>
        /// <param name="registerRequest">Information about user: email, name, and password.</param>
        /// <param name="cancellationToken">Token to cancel operation.</param>
        /// <returns>Returns AuthResponse with access and refresh tokens and user information.</returns>
        /// <response code="200">Successful registration.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.RegisterAsync(registerRequest, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        /// <summary>
        /// Login in sustem.
        /// </summary>
        /// <param name="loginRequest">User credentials: email and password.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Returns an AuthResponse containing access and refresh tokens and user information.</returns>
        /// <response code="200">Successful login.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.LoginAsync(loginRequest, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        /// <summary>
        /// Refresh access token using refresh token.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Returns a new AuthResponse with refreshed access and refresh tokens.</returns>
        /// <response code="200">Tokens refreshed successfully.</response>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {
            var accessToken = Request.Cookies["access-token"];
            var refreshToken = Request.Cookies["refresh-token"];

            var authResponse = await _authService.RefreshAsync(accessToken, refreshToken, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        /// <summary>
        /// Sends a password reset email to the user.
        /// </summary>
        /// <param name="request">Contains user email</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Returns OK if the email was successfully sent.</returns>
        /// <response code="200">Password reset email sent.</response>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            await _passwordResetService.SendResetEmailMessageAsync(request.Email, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Resets user password using a reset token.
        /// </summary>
        /// <param name="request">Contains email, reset token, and new password.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>Returns OK if the password was successfully reset.</returns>
        /// <response code="200">Password reset successfully.</response>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            await _passwordResetService.ResetPasswordAsync(request.Email, request.Token, request.Password, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Returns infromation about user.
        /// </summary>
        /// <returns> Infromation about user.</returns>
        /// <response code="200">User information recieved successfully.</response>
        /// <response code="401">Unauthorized if user not authenticated.</response>
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult GetCurrentUser()
        {
            var user = HttpContext.User;

            var userId = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            var userName = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var role = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Id = userId,
                Name = userName,
                Email = email,
                Role = role
            });
        }

        /// <summary>
        /// Set access and refresh tokens in cookies.
        /// </summary>
        /// <param name="authResponse">AuthResponse containing tokens to set in cookies.</param>
        private void SetCookie(AuthResponse authResponse)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = authResponse.RefreshToken.ExpiresAt
            };

            HttpContext.Response.Cookies.Append("access-token", authResponse.AccessToken, cookieOptions);
            HttpContext.Response.Cookies.Append("refresh-token", authResponse.RefreshToken.Token, cookieOptions);
        }
    }
}
