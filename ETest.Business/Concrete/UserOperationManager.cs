using System.Threading.Tasks;
using Core.Constants;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.DataAccess.Abstract;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class UserOperationManager:IUserOperationService
    {
        private readonly IUserDal _userDal;

        public UserOperationManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<Result> UserNameExistsAsync(string userName)
        {
            var isExists = await _userDal.AnyAsync(x => x.UserName == userName);
            return isExists ? (Result) new ErrorResult(Messages.UserExists) : new SuccessResult(Messages.UserNotExists);
        }

        public async Task<Result> IsActiveAsync(string email)
        {
            var user = await _userDal.GetAsync(x => x.Email == email);
            if(user.IsActive)
                return new SuccessResult(Messages.Success);
            return new ErrorResult(Messages.UserNotActive);
        }

        public async Task<IDataResult<User>> GetUserByEmailAsync(string email)
        {
            var user = await _userDal.GetAsync(x => x.Email == email);
            return BusinessResultHelper.ReturnData(user, Messages.Found, Messages.NotFound);
        }

        public async Task<Result> EmailExistsAsync(string email)
        {
            var isExists = await _userDal.AnyAsync(x => x.Email == email);
            return isExists ? (Result) new ErrorResult(Messages.UserExists) : new SuccessResult(Messages.UserNotExists);
        }
    }
}