using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Entities.Concrete;

namespace ETest.DataAccess.Repositories
{
    public class UserOperationClaimRepository:EfRepositoryBase<UserOperationClaim,ETestContext>,IUserOperationClaimDal
    {
        public UserOperationClaimRepository(ETestContext context) : base(context)
        {
        }
    }
}