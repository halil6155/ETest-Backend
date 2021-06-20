using System.Net;
using System.Threading.Tasks;
using ETest.Business.Abstract;
using ETest.Dto.Category;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(CategoryForAddDto categoryForAddDto)
            => Result(await _categoryService.AddAsync(categoryForAddDto),(int)HttpStatusCode.Created);
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(CategoryForUpdateDto categoryForUpdateDto)
            => Result(await _categoryService.UpdateAsync(categoryForUpdateDto), (int)HttpStatusCode.NoContent);

        [HttpGet("getList")]
        public IActionResult GetList() =>
            DataResult(_categoryService.GetList
                ());
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) =>
            DataResult(await _categoryService.GetBydIdAsync(id));
    }
}
