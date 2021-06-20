using System.Linq;
using System.Threading.Tasks;
using Core.DataAccess.Concrete.EntityFramework;
using ETest.DataAccess.Abstract;
using ETest.DataAccess.Concrete.Contexts;
using ETest.Entities.Concrete;
using ETest.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ETest.DataAccess.Repositories
{
    public class UserRepository:EfRepositoryBase<User,ETestContext>,IUserDal
    {
        public UserRepository(ETestContext context) : base(context)
        {
        }
        public async Task<UserOperationClaimModel> GetClaimsAsync(User user)
        {
            var operationClaims = from operationClaim in Context.OperationClaims
                join userOperationClaim in Context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == user.Id
                select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            var userOperationClaimModel = new UserOperationClaimModel
            {
                User = user,
                OperationClaims = await operationClaims.ToListAsync()
            };
            return userOperationClaimModel;

        }
    }
}