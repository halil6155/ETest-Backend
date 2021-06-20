using System.Net;
using System.Threading.Tasks;
using ETest.Business.Abstract;
using ETest.Dto.UserAnswer;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAnswersController : BaseApiController
    {
        private readonly IUserAnswerService _userAnswerService;

        public UserAnswersController(IUserAnswerService userAnswerService)
        {
            _userAnswerService = userAnswerService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(UserAnswerForAddDto userAnswerForAddDto)
            => Result(await _userAnswerService.AddAsync(userAnswerForAddDto), (int)HttpStatusCode.Created);

        [HttpGet("getResult/{categoryId}/{userId}")]
        public async Task<IActionResult> GetResultAsync(int categoryId, int userId) =>
            DataResult(await _userAnswerService.GetUserResultAsync(categoryId, userId));

        [HttpGet("getUserResults/{userId}")]
        public async Task<IActionResult> GetUserResults(int userId)
            => DataResult(await _userAnswerService.GetUserResultsAsync(userId));

        [HttpGet("getMainResults/{userId}")]
        public async Task<IActionResult> GetMainResultsAsync(int userId)
            => DataResult(await _userAnswerService.GetUserAnswerMainResultsAsync(userId));
    }
}
