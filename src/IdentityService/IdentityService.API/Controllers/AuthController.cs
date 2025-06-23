using AutoMapper;
using CatalogService.API.Models;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
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
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.RegisterAsync(request, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var authResponse = await _authService.LoginAsync(request, cancellationToken);

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
