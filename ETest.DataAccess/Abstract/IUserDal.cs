using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using ETest.Entities.Concrete;
using ETest.Entities.Models;

namespace ETest.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
        Task<UserOperationClaimModel> GetClaimsAsync(User user);
    }
}