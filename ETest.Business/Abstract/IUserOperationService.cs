using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Entities.Concrete;

namespace ETest.Business.Abstract
{
    public interface IUserOperationService
    {
        Task<Result> UserNameExistsAsync(string userName);
        Task<Result> IsActiveAsync(string email);
        Task<IDataResult<User>> GetUserByEmailAsync(string email);
        Task<Result> EmailExistsAsync(string email);
    }
}