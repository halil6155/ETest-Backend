using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using Core.Utilities.Security.Token.Concrete;
using ETest.Entities.Concrete;

namespace ETest.Business.Abstract
{
    public interface ITokenService
    {
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
        Task<Result> RemoveRefreshTokenAsync(User user);
        Task<IDataResult<AccessToken>> GetUserWithRefreshTokenAsync(string refreshToken);
        Task<Result> SaveRefreshTokenAsync(User user, string refreshToken);
    }
}