using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Api;
using Core.Utilities.Results.Concrete.Status;
using Microsoft.AspNetCore.Mvc;

namespace ETest.API.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IActionResult DataResult<T>(IDataResult<T> dataResult,int statusCode=0, string internalMessage = "İstek Başarılı")
        {

            if (statusCode <= 0)
                statusCode = dataResult.Success ? 200 : 400;
            return StatusCode(statusCode, new ApiDataResult<T>
            {
                Success = dataResult.Success,
                Message = dataResult.Message,
                InternalMessage = internalMessage,
                Data = dataResult.Data
            });
        }
        protected IActionResult Result(Result result, int statusCode = 0, string internalMessage = "İstek Başarılı")
        {
            if (statusCode <= 0)
                statusCode = result.Success ? 200 : 400;
            return StatusCode(statusCode, new ApiResult
            {
                Success = result.Success,
                Message = result.Message,
                InternalMessage = internalMessage,
            });
        }
    }
}
