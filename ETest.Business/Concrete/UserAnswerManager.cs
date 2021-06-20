using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.Business.ValidationRules.FluentValidation.UserAnswer;
using ETest.DataAccess.Abstract;
using ETest.Dto.UserAnswer;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class UserAnswerManager : IUserAnswerService
    {
        private readonly IUserAnswerDal _userAnswerDal;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public UserAnswerManager(IUserAnswerDal userAnswerDal, IMapper mapper, IQuestionService questionService)
        {
            _userAnswerDal = userAnswerDal;
            _mapper = mapper;
            _questionService = questionService;
        }
        [ValidationAspect(typeof(UserAnswerForAddDtoValidator))]
        public async Task<Result> AddAsync(UserAnswerForAddDto userAnswerForAddDto)
        {
            var questionSingleDto = await _questionService.GetByIdAsync(userAnswerForAddDto.QuestionId);
            if (questionSingleDto == null) return new ErrorResult(Messages.NotAdded);
            var userAnswer = _mapper.Map<UserAnswer>(userAnswerForAddDto);
            userAnswer.IsTrue = questionSingleDto.Data.Answer == userAnswer.Answer;
            await _userAnswerDal.AddAsync(
                userAnswer);
            return BusinessResultHelper.ReturnResult(await _userAnswerDal.SaveChangesAsync(), Messages.Added,
                Messages.NotAdded);
        }

        public async Task<IDataResult<List<UserAnswerForMainResultListDto>>> GetUserAnswerMainResultsAsync(int userId)
        {
            return BusinessResultHelper.ReturnData(await _userAnswerDal.GetMainResultAsync(userId), Messages.Found,
                Messages.NotFound);
        }

        public async Task<IDataResult<UserAnswerForResultDto>> GetUserResultAsync(int categoryId, int userId)
        {
            var userAnswers = await _userAnswerDal.GetUserResultAsync(categoryId, userId);
            if (userAnswers == null) return new ErrorDataResult<UserAnswerForResultDto>(Messages.NotFound);
            var userAnswerForResultDto = new UserAnswerForResultDto
            {
                TotalQuestion = userAnswers.Count,
                WrongNumber = userAnswers.Count(x => !x.IsTrue),
                CorrectNumber = userAnswers.Count(x => x.IsTrue),
            };
            return BusinessResultHelper.ReturnData(userAnswerForResultDto, Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<List<UserAnswerForListResultDto>>> GetUserResultsAsync(int userId)
        {
            return BusinessResultHelper.ReturnData(await _userAnswerDal.GetUserResultsAsync(userId), Messages.Found,
                Messages.NotFound);
        }
    }
}