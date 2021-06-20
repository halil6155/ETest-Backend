using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.Business.Aspects.Autofac;
using ETest.Business.ValidationRules.FluentValidation.Question;
using ETest.DataAccess.Abstract;
using ETest.Dto.Question;
using ETest.Entities.Concrete;
using ETest.Entities.Models;

namespace ETest.Business.Concrete
{
    public class QuestionManager:IQuestionService

    {
        private readonly IMapper _mapper;
        private readonly IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal, IMapper mapper)
        {
            _questionDal = questionDal;
            _mapper = mapper;
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        [ValidationAspect(typeof(QuestionForAddDtoValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<Result> AddAsync(QuestionForAddDto questionForAddDto)
        {
            var question = _mapper.Map<Question>(questionForAddDto);
            question.CreatedOn=DateTime.Now;
            await _questionDal.AddAsync(question);
            return BusinessResultHelper.ReturnResult(_questionDal.SaveChangesAsync(), Messages.Added,
                Messages.NotAdded);
        }

        public async Task<int> CountAsync()
        {
            return await _questionDal.CountAsync();
        }

        public async Task<IDataResult<QuestionForSingleDto>> GetByIdAsync(int id)
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<QuestionForSingleDto>(await _questionDal.GetAsync(x => x.Id == id)),
                Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<QuestionForSingleDto>> GetByCategoryAndUserIdAsync(int categoryId, int userId)
        {
         
           return BusinessResultHelper.ReturnData(_mapper.Map<QuestionForSingleDto>(await _questionDal.GetUserAndCategoryIdAsync(categoryId,userId)),
                Messages.Found, Messages.NotFound);
        }
        [CacheAspect()]
        public IDataResult<List<QuestionForListDto>> GetList()
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<List<QuestionForListDto>>(_questionDal.GetListAsync().Result), Messages.Found, Messages.NotFound);
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        [ValidationAspect(typeof(QuestionForUpdateDtoValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<Result> UpdateAsync(QuestionForUpdateDto questionForUpdateDto)
        {
            var question = await _questionDal.GetAsync(x => x.Id == questionForUpdateDto.Id);
            question.ModifiedOn=DateTime.Now;
            _questionDal.Update(question);
            return BusinessResultHelper.ReturnResult(_questionDal.SaveChangesAsync(), Messages.Updated,
                Messages.NotUpdated);
        }
        
        public async Task<IDataResult<QuestionListModel>> GetListByFiltersAsync(QuestionForFilterDto questionForFilterDto)
        {
            var result = await _questionDal.GetQuestionsByFiltersAsync(questionForFilterDto);
            if (result != null)
            {
                var questionForListDto = _mapper.Map<List<QuestionForListDto>>(result.Item1);
                return new SuccessDataResult<QuestionListModel>(new QuestionListModel
                {
                    ListItems = questionForListDto,
                    TotalCount = result.Item2
                });
            }
            return new ErrorDataResult<QuestionListModel>(Messages.NotFound);
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public async Task<Result> DeleteByIdAsync(int id)
        {
            var question =await _questionDal.GetAsync(x => x.Id == id);
            if (question==null)return new ErrorResult(Messages.NotFound);
            _questionDal.Delete(question);
            return BusinessResultHelper.ReturnResult(_questionDal.SaveChangesAsync(), Messages.Deleted,
                Messages.NotDeleted);
        }
    }
}