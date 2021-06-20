using System.Net;
using System.Threading.Tasks;
using Core.Extensions;
using ETest.Business.Abstract;
using ETest.Dto.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetListAsync()
            => DataResult(await _userService.GetListAsync());

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
            => DataResult(await _userService.GetUserSingleDtoByIdAsync(id));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(UserForUpdateDto userForUpdateDto) =>
            Result(await _userService.UpdateAsync(userForUpdateDto),(int)HttpStatusCode.NoContent);
        [HttpPost("imageUpload")]
        public async Task<IActionResult> ImageUploadAsync(IFormFile image)
            => Result(await _userService.ImageUploadAsync(image,User.ClaimNameIdentifier()));
    }
}
