using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.Question;
using ETest.Entities.Models;

namespace ETest.Business.Abstract
{
    public interface IQuestionService
    {
        Task<Result> AddAsync(QuestionForAddDto questionForAddDto);
        Task<int> CountAsync();
        Task<IDataResult<QuestionForSingleDto>> GetByIdAsync(int id);
        Task<IDataResult<QuestionForSingleDto>> GetByCategoryAndUserIdAsync(int categoryId,int userId);
        IDataResult<List<QuestionForListDto>> GetList();
        Task<Result> UpdateAsync(QuestionForUpdateDto questionForUpdateDto);
        Task<IDataResult<QuestionListModel>> GetListByFiltersAsync(QuestionForFilterDto questionForFilterDto);
        Task<Result> DeleteByIdAsync(int id);
    }
}