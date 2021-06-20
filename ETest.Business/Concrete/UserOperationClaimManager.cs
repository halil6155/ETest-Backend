using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Constants;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.Business.Aspects.Autofac;
using ETest.DataAccess.Abstract;
using ETest.Dto.UserOperationClaim;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class UserOperationClaimManager:IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _userService = userService;
        }
        [SecuredOperation("Admin", typeof(FileLogger))]
        public async Task<Result> AddAsync(List<UserOperationClaimForAddDto> userOperationsClaimDto)
        {
            var userResult = await _userService.GetByIdAsync(userOperationsClaimDto[0].UserId);
            if (!userResult.Success) return new ErrorResult(userResult.Message);
            foreach (var operationClaimDto in userOperationsClaimDto)
            {
                if (operationClaimDto.IsCheck)
                {
                    if (await _userOperationClaimDal.AnyAsync(x =>
                        x.UserId == userResult.Data.Id &&
                        x.OperationClaimId == operationClaimDto.OperationClaimId) == false)
                    {
                        await _userOperationClaimDal.AddAsync(new UserOperationClaim
                            { OperationClaimId = operationClaimDto.OperationClaimId, UserId = userResult.Data.Id });
                    }
                }
                else
                {
                    var deletedUserOperationClaimResult =
                        await GetByUserAndOperationClaimId(userResult.Data.Id, operationClaimDto.OperationClaimId);
                    if (deletedUserOperationClaimResult.Success)
                        Delete(deletedUserOperationClaimResult.Data);
                }
            }

            return BusinessResultHelper.ReturnResult(await _userOperationClaimDal.SaveChangesAsync(),
                Messages.Added, Messages.NotAdded);
        }
        public IDataResult<UserOperationClaim> Delete(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
            return BusinessResultHelper.ReturnData(userOperationClaim, Messages.Deleted, Messages.NotDeleted);
        }
        public async Task<IDataResult<UserOperationClaim>> GetByUserAndOperationClaimId(int userId, int operationClaimId)
        {
            var userOperationClaim =
                await _userOperationClaimDal.GetAsync(x =>
                    x.UserId == userId && x.OperationClaimId == operationClaimId);
            return BusinessResultHelper.ReturnData(userOperationClaim, Messages.Found, Messages.NotFound);
        }
    }
}