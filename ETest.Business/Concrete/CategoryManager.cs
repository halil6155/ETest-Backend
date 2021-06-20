using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.Business.Aspects.Autofac;
using ETest.Business.ValidationRules.FluentValidation.Category;
using ETest.DataAccess.Abstract;
using ETest.Dto.Category;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private  readonly IMapper _mapper;


        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;

        }
        [SecuredOperation("Admin",typeof(FileLogger))]
        [ValidationAspect(typeof(CategoryForAddDtoValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<Result> AddAsync(CategoryForAddDto categoryForAddDto)
        {
           var category = _mapper.Map<Category>(categoryForAddDto);
            category.CreatedOn=DateTime.Now;
            await _categoryDal.AddAsync(category);
            return BusinessResultHelper.ReturnResult(await _categoryDal.SaveChangesAsync(), Messages.Added,
                Messages.NotAdded);
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        [ValidationAspect(typeof(CategoryForUpdateDtoValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public async Task<Result> UpdateAsync(CategoryForUpdateDto categoryForUpdateDto)
        {
            var category = await _categoryDal.GetAsync(x => x.Id == categoryForUpdateDto.Id);
            if(category==null)return new ErrorResult(Messages.NotFound);
            category = _mapper.Map(categoryForUpdateDto, category);
            _categoryDal.Update(category);
            return BusinessResultHelper.ReturnResult(await _categoryDal.SaveChangesAsync(), Messages.Updated,
                Messages.NotUpdated);
        }

        public async Task<int> CountAsync()
        {
            return await _categoryDal.CountAsync();
        }
        
        [CacheAspect()]
        [PerformanceAspect(1,typeof(FileLogger))]
        public IDataResult<List<CategoryForListDto>> GetList()
        {
            var categoryList =  _categoryDal.GetListAsync().Result;
            var categoryListDto = _mapper.Map <List<CategoryForListDto>>(categoryList);
       
            return BusinessResultHelper.ReturnData(categoryListDto, Messages.Found, Messages.NotFound);
      
        }

        public async Task<IDataResult<CategoryForListDto>> GetBydIdAsync(int id)
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<CategoryForListDto>(await _categoryDal.GetAsync(x=>x.Id==id)), Messages.Found, Messages.NotFound);
        }
    }
}