using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using Core.Utilities.Security.Token.Abstract;
using Core.Utilities.Security.Token.Concrete;
using ETest.Business.Abstract;
using ETest.Entities.Concrete;
using Microsoft.Extensions.Configuration;

namespace ETest.Business.Concrete
{
    public class TokenManager:ITokenService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserService _userService;
        public IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        public TokenManager(ITokenHelper tokenHelper, IConfiguration configuration, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            Configuration = configuration;
            _userService = userService;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var userClaims =await _userService.GetUserClaimsByIdAsync(user.Id);
            var accessToken = _tokenHelper.CreateAccessToken(new UserHelperPartialDto { Username = user.UserName, Id = user.Id, FullName = $"{user.FirstName } {user.LastName}", Roles = GetUserClaimsConvertToArray(userClaims.Data.OperationClaims) });
            await SaveRefreshTokenAsync(user, accessToken.RefreshToken);
            return BusinessResultHelper.ReturnData(accessToken, Messages.Created, Messages.NotCreated);
        }

        private string[] GetUserClaimsConvertToArray(List<OperationClaim> operationClaims)
        {
            if (operationClaims == null || operationClaims.Count <= 0)
                return new[] {""};
            return operationClaims.Select(x => x.Name).ToArray();
        }
        public async Task<Result> RemoveRefreshTokenAsync(User user)
        {
            user.RefreshToken = null;
            user.RefreshTokenExpiration = null;
            return await _userService.UpdateByUserAsync(user);
        }

        public async Task<IDataResult<AccessToken>> GetUserWithRefreshTokenAsync(string refreshToken)
        {
            var user = await _userService.GetUserByRefreshTokenAsync(refreshToken);
            if (!user.Success) return new ErrorDataResult<AccessToken>(user.Message);
            if (DateTime.Now > user.Data.RefreshTokenExpiration)
                return new ErrorDataResult<AccessToken>(Messages.UserRefreshTokenDateExpirated);
            return await CreateAccessTokenAsync(user.Data);
        }

        public async Task<Result> SaveRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);
            return await _userService.UpdateByUserAsync(user);
        }
    }
}