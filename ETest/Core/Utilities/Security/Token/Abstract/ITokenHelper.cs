using Core.Entities.Concrete;
using Core.Utilities.Security.Token.Concrete;

namespace Core.Utilities.Security.Token.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateAccessToken(UserHelperPartialDto userHelperPartialDto);
    }
}