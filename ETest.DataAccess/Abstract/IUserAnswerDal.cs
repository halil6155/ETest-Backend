using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using ETest.Dto.UserAnswer;
using ETest.Entities.Concrete;

namespace ETest.DataAccess.Abstract
{
    public interface IUserAnswerDal:IEntityRepository<UserAnswer>
    {
        Task<List<UserAnswer>> GetUserResultAsync(int categoryId, int userId);
        Task<List<UserAnswerForMainResultListDto>> GetMainResultAsync(int userId);
        Task<List<UserAnswerForListResultDto>> GetUserResultsAsync(int userId);
    }
}