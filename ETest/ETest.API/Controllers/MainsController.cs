using System.Threading.Tasks;
using ETest.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainsController : BaseApiController
    {
        private readonly IMainService _mainService;

        public MainsController(IMainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("mainResult")]
        public async Task<IActionResult> GetMainSingleDtoAsync() =>
            DataResult(await _mainService.GetMainSingleDtoAsync());

    }
}
