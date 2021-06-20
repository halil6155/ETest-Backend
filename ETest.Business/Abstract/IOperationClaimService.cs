using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.OperationClaim;
using ETest.Entities.Concrete;

namespace ETest.Business.Abstract
{
    public interface IOperationClaimService
    {
        Task<Result> AddAsync(OperationClaimForAddDto operationClaimForAddDto);
        Task<IDataResult<OperationClaimForSingleDto>> GetByIdAsync(int id);
        Task<IDataResult<List<OperationClaim>>> GetOperationClaimsAsync();
        Task<Result> UpdateAsync(OperationClaimForUpdateDto operationClaimForUpdateDto);
        IDataResult<List<OperationClaimForListDto>> GetList();
    }
}