using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ETest.Business.Abstract;
using ETest.Dto.OperationClaim;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseApiController
    {
        private readonly IOperationClaimService _operationClaimService;
        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(OperationClaimForAddDto operationClaimForAddDto)
            => Result(await _operationClaimService.AddAsync(operationClaimForAddDto), (int)HttpStatusCode.Created);

        [HttpGet("getList")]
        public IActionResult GetList() =>
            DataResult(_operationClaimService.GetList());
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(OperationClaimForUpdateDto operationClaimForUpdateDto)
            => Result(await _operationClaimService.UpdateAsync(operationClaimForUpdateDto), (int)HttpStatusCode.NoContent);

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
            => DataResult(await _operationClaimService.GetByIdAsync(id));
    }
}
