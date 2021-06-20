using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Dto.UserAnswer;
using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETest.DataAccess.Repositories
{
    public class UserAnswerRepository:EfRepositoryBase<UserAnswer,ETestContext>,IUserAnswerDal
    {
        public UserAnswerRepository(ETestContext context) : base(context)
        {
        }

        public async Task<List<UserAnswerForMainResultListDto>> GetMainResultAsync(int userId)
        {
            var queryable = Context.UserAnswers.Where(x=>x.UserId==userId).Include(x => x.Question).ThenInclude(x => x.Category)
                .GroupBy(x => x.Question.Category.Name).Select(x => new UserAnswerForMainResultListDto
                {
                  CategoryName = x.Key,
                  TotalQuestion =x.Count(i=>i.IsTrue)+x.Count(i=>!i.IsTrue),
                  WrongNumber = x.Count(i => !i.IsTrue),
                  CorrectNumber = x.Count(i => i.IsTrue)
                });
            return await queryable.ToListAsync();
        }
        public async Task<List<UserAnswer>> GetUserResultAsync(int categoryId,int userId)
        {
            var userAnswers = await Context.UserAnswers.Include(x => x.Question)
                .Where(x => x.UserId == userId && x.Question.CategoryId == categoryId).ToListAsync();
             return userAnswers;
        }

        public async Task<List<UserAnswerForListResultDto>> GetUserResultsAsync(int userId)
        {
            var result=await Context.UserAnswers.Where(x => x.UserId == userId)
                .GroupBy(x => x.Question.Category.Name)
                .Select(x => new UserAnswerForListResultDto
                {
                   CategoryName= x.Key,
                   WrongNumber = x.Count(i=>!i.IsTrue),
                   CorrectNumber = x.Count(i => i.IsTrue),
                 TotalQuestion  = x.Count(),
                }).ToListAsync();
            return result;
        }
    }
}