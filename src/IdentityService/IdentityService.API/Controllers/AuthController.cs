using AutoMapper;
using CatalogService.API.Models;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
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

            var accessToken = await _authService.RegisterAsync(registerDTO, cancellationToken);

            return Ok(accessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel, CancellationToken cancellationToken)
        {
            var loginDTO = _mapper.Map<LoginRequest>(loginModel);

            var accessToken = await _authService.LoginAsync(loginDTO, cancellationToken);

            return Ok(accessToken);
        }



    }
}
