using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Entities.Concrete;

namespace ETest.DataAccess.Repositories
{
    public class OperationClaimRepository:EfRepositoryBase<OperationClaim,ETestContext>,IOperationClaimDal
    {
        public OperationClaimRepository(ETestContext context) : base(context)
        {
        }
    }
}