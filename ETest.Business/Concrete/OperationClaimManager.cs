using System.Collections.Generic;
using System.Linq;
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
using ETest.Business.ValidationRules.FluentValidation.OperationClaim;
using ETest.DataAccess.Abstract;
using ETest.Dto.OperationClaim;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class OperationClaimManager:IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IMapper _mapper;

        public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(OperationClaimForAddDtoValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]
        public async Task<Result> AddAsync(OperationClaimForAddDto operationClaimForAddDto)
        {
            var operationClaim = _mapper.Map<OperationClaim>(operationClaimForAddDto);
            await _operationClaimDal.AddAsync(operationClaim);
            return BusinessResultHelper.ReturnResult(await _operationClaimDal.SaveChangesAsync(), Messages.Added,
                Messages.NotAdded);
        }

        public async Task<IDataResult<List<OperationClaim>>> GetOperationClaimsAsync()
        {
            var result = await _operationClaimDal.GetListAsync();
            return BusinessResultHelper.ReturnData(result.ToList(), Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<OperationClaimForSingleDto>> GetByIdAsync(int id)
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<OperationClaimForSingleDto>(await _operationClaimDal.GetAsync(x=>x.Id==id)), Messages.Found,
                Messages.NotFound);
        }

        [SecuredOperation("Admin", typeof(FileLogger))]

        [ValidationAspect(typeof(OperationClaimForUpdateDtoValidator))]
        [CacheRemoveAspect("IOperationClaimService.Get")]

        public async Task<Result> UpdateAsync(OperationClaimForUpdateDto operationClaimForUpdateDto)
        {
            var operationClaim = _mapper.Map<OperationClaim>(operationClaimForUpdateDto);
            _operationClaimDal.Update(operationClaim);
            return BusinessResultHelper.ReturnResult(await _operationClaimDal.SaveChangesAsync(), Messages.Updated,
                Messages.NotUpdated);
        }

        public IDataResult<List<OperationClaimForListDto>> GetList()
        {
            return BusinessResultHelper.ReturnData(
                _mapper.Map<List<OperationClaimForListDto>>(_operationClaimDal.GetListAsync().Result), Messages.Found,
                Messages.NotFound);
        }
    }
}