using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using Core.Utilities.Security.Token.Concrete;
using ETest.Dto.User;
using ETest.Entities.Concrete;

namespace ETest.Business.Abstract
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<AccessToken>> GetUserWithRefreshTokenAsync(string refreshToken);
        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
    }
}