using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETest.Business.Abstract;
using ETest.Dto.Token;
using ETest.Dto.User;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserForRegisterDto userForRegisterDto)
            => Result(await _authService.RegisterAsync(userForRegisterDto), (int) HttpStatusCode.Created);
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var user = await _authService.LoginAsync(userForLoginDto);
        
            if (!user.Success) return DataResult(user);
            var accessToken = await _tokenService.CreateAccessTokenAsync(user.Data);
            return DataResult(accessToken);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            if (string.IsNullOrEmpty(refreshTokenDto.RefreshToken)) return BadRequest("Hatalı Token!");
            return DataResult(await _authService.GetUserWithRefreshTokenAsync(refreshTokenDto.RefreshToken));
        }
    }
}
