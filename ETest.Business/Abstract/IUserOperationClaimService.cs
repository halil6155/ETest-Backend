using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.UserOperationClaim;
using ETest.Entities.Concrete;

namespace ETest.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<Result> AddAsync(List<UserOperationClaimForAddDto> userOperationsClaimDto);
        Task<IDataResult<UserOperationClaim>> GetByUserAndOperationClaimId(int userId, int operationClaimId);
        IDataResult<UserOperationClaim> Delete(UserOperationClaim userOperationClaim);
    }
}