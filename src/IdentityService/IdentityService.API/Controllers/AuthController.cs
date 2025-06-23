using System.Security.Claims;
using AutoMapper;
using IdentityService.API.Filters;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using IdentityService.DAL.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPasswordResetService _passwordResetService;

        public AuthController(IAuthService authService, IPasswordResetService passwordResetService)
        {
            _authService = authService;
            _passwordResetService = passwordResetService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest regiserRequest, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.RegisterAsync(regiserRequest, cancellationToken);

            SetCookie(authResponse);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.LoginAsync(loginRequest, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
        {

            var accessToken = Request.Cookies["access-token"];

            var refreshToken = Request.Cookies["refresh-token"];

            var authResponce = await _authService.RefreshAsync(accessToken, refreshToken, cancellationToken);

            SetCookie(authResponce);

            return Ok(authResponce);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
        {
            await _passwordResetService.SendResetEmailMessageAsync(request.Email, cancellationToken);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            await _passwordResetService.ResetPasswordAsync(request.Email, request.Token, request.Password, cancellationToken);

            return Ok();
        }


        [HttpGet]
        [Role(nameof(UserRoles.User))]
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

        private void SetCookie(AuthResponse authResponse)
        {
            var cookieOptions = new CookieOptions()
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
