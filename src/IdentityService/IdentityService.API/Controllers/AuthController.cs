using System.Security.Claims;
using AutoMapper;
using CatalogService.API.Models;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest regiserRequest, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.RegisterAsync(regiserRequest, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
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

        [HttpGet]
        [Authorize]
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
