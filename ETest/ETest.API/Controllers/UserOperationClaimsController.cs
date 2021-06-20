using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ETest.Business.Abstract;
using ETest.Dto.UserOperationClaim;
 
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseApiController
    {
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService, IUserService userService)
        {
            _userOperationClaimService = userOperationClaimService;
            _userService = userService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(List<UserOperationClaimForAddDto> userOperationsClaimDto)
            => Result(await _userOperationClaimService.AddAsync(userOperationsClaimDto),(int)HttpStatusCode.Created);
        [HttpGet("getUserClaims/{id}")]
        public async Task<IActionResult> GetUserClaimsAsync(int id)
            => DataResult(await _userService.GetUserOperationClaimDtoByIdAsync(id));
    }
}
