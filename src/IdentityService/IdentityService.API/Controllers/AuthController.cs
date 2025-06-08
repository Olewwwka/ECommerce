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
        public async Task<IActionResult> Register(RegisterModel regiserUser, CancellationToken cancellationToken)
        {
            var registerDTO = _mapper.Map<RegisterRequest>(regiserUser);

            var authResponse = await _authService.RegisterAsync(registerDTO, cancellationToken);

            SetCookie(authResponse);

            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel, CancellationToken cancellationToken)
        {
            var loginDTO = _mapper.Map<LoginRequest>(loginModel);

            var authResponse = await _authService.LoginAsync(loginDTO, cancellationToken);

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
