using System;
using System.Threading.Tasks;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using ETest.Business.Abstract;
using ETest.Business.Aspects.Autofac;
using ETest.Dto.Main;

namespace ETest.Business.Concrete
{
    public class MainManager : IMainService
    {
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;
        private readonly ICategoryService _categoryService;

        public MainManager(IUserService userService, ICategoryService categoryService, IQuestionService questionService)
        {
            _userService = userService;
            _categoryService = categoryService;
            _questionService = questionService;
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        public async Task<IDataResult<MainSingleDto>> GetMainSingleDtoAsync()
        {
            var mainSingleDto = CreateMainSingleDto(await _userService.UserCountByStatus(true), await _userService.UserCountByStatus(false),
                 await _categoryService.CountAsync(), await _questionService.CountAsync());
            return BusinessResultHelper.ReturnData(mainSingleDto, Messages.Found, Messages.NotFound);

        }

        private MainSingleDto CreateMainSingleDto(int userActiveCount, int userDisabledCount, int categoryCount, int questionCount)
        {
            return new MainSingleDto
            {
                ActiveUser = userActiveCount,
                DisabledUser = userDisabledCount,
                TotalCategory = categoryCount,
                TotalQuestion = questionCount
            };
        }


    }
}