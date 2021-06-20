using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Dto.Question;
using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETest.DataAccess.Repositories
{
    public class QuestionRepository:EfRepositoryBase<Question,ETestContext>,IQuestionDal
    {
        public QuestionRepository(ETestContext context) : base(context)
        {
        }

        public async Task<Tuple<List<Question>, int>> GetQuestionsByFiltersAsync(QuestionForFilterDto filters)
        {
           return  await ApplyFilterAsync(Context.Questions.AsQueryable(), filters);
        }

        private async Task<Tuple<List<Question>, int>> ApplyFilterAsync(IQueryable<Question> queryable,QuestionForFilterDto filters)
        {
            var totalCount =await queryable.CountAsync();
            var questions= await queryable.Skip((filters.CurrentPage - 1) * filters.PageSize).Take(filters.PageSize)
                .ToListAsync();
            return new Tuple<List<Question>, int>(questions,totalCount);
        }

        public async Task<Question> GetUserAndCategoryIdAsync(int categoryId,int userId)
        {
            var queryable = Context.Questions.Where(x => x.CategoryId == categoryId).Include(x=>x.UserAnswers);
            var question =
                await queryable.FirstOrDefaultAsync(predicate: x => x.UserAnswers.All(i => i.UserId != userId));
           return question;
        }
    }
}