using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.UserAnswer;

namespace ETest.Business.Abstract
{
    public interface IUserAnswerService
    {
        Task<Result> AddAsync(UserAnswerForAddDto userAnswerForAddDto);
        Task<IDataResult<List<UserAnswerForMainResultListDto>>> GetUserAnswerMainResultsAsync(int userId);
        Task<IDataResult<UserAnswerForResultDto>> GetUserResultAsync(int categoryId, int userId);
        Task<IDataResult<List<UserAnswerForListResultDto>>> GetUserResultsAsync(int userId);
    }
}