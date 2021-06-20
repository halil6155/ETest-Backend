using System.Net;
using System.Threading.Tasks;
using ETest.Business.Abstract;
using ETest.Dto.Question;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : BaseApiController
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(QuestionForAddDto questionForAddDto)
            => Result(await _questionService.AddAsync(questionForAddDto), (int) HttpStatusCode.Created);

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
            => Result(await _questionService.DeleteByIdAsync(id));

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(QuestionForUpdateDto questionForUpdateDto)
            => Result(await _questionService.UpdateAsync(questionForUpdateDto), (int) HttpStatusCode.NoContent);

        [HttpGet("getListByFilter/{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetListByFilterAsync([FromQuery]QuestionForFilterDto questionForFilterDto)
            => DataResult(await _questionService.GetListByFiltersAsync(questionForFilterDto));

        [HttpGet("getList")]
        public IActionResult GetList() => DataResult(_questionService.GetList());

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) => DataResult(await _questionService.GetByIdAsync(id));
        [HttpGet("getByUserAndCategoryId/{categoryId}/{userId}")]
        public async Task<IActionResult> GetByUserAndCategoryId(int categoryId,int userId) =>
            DataResult(await _questionService.GetByCategoryAndUserIdAsync(categoryId,userId));
    }
}
