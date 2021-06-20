using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using ETest.Dto.Question;
using ETest.Entities.Concrete;

namespace ETest.DataAccess.Abstract
{
    public interface IQuestionDal:IEntityRepository<Question>
    {
        Task<Tuple<List<Question>, int>> GetQuestionsByFiltersAsync(QuestionForFilterDto filters);
        Task<Question> GetUserAndCategoryIdAsync(int categoryId, int userId);
    }
}