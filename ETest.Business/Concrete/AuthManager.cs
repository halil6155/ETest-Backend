using System;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Token.Concrete;
using ETest.Business.Abstract;
using ETest.Business.ValidationRules.FluentValidation.User;
using ETest.DataAccess.Abstract;
using ETest.Dto.User;
using ETest.Entities.Concrete;

namespace ETest.Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUserOperationService _userOperationService;

        public AuthManager(IUserDal userDal, IMapper mapper, IUserOperationService userOperationService, ITokenService tokenService)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userOperationService = userOperationService;
            _tokenService = tokenService;
        }
        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        public async Task<Result> RegisterAsync(UserForRegisterDto userForRegisterDto)
        {
            var isCheck = BusinessRules.Run(await _userOperationService.UserNameExistsAsync(userForRegisterDto.UserName),
                await _userOperationService.EmailExistsAsync(userForRegisterDto.Email));
            if (isCheck != null) return new ErrorResult(isCheck.Message);
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out var passwordHash, out var passwordSalt);
            var user = _mapper.Map<User>(userForRegisterDto);
            user.CreatedOn=DateTime.Now;
            user.IsActive = true;
            user.ImageUrl = "default-user.jpeg";
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _userDal.AddAsync(user);
            return BusinessResultHelper.ReturnResult(await _userDal.SaveChangesAsync(), Messages.Added, Messages.NotAdded);
        }

        public async Task<IDataResult<AccessToken>> GetUserWithRefreshTokenAsync(string refreshToken)
        {
            var user = await _userDal.GetAsync(x => x.RefreshToken == refreshToken);
            if (user == null)
                return new ErrorDataResult<AccessToken>(null, Messages.NotFound);
            if (DateTime.Now > user.RefreshTokenExpiration)
                return new ErrorDataResult<AccessToken>(null, Messages.UserRefreshTokenDateExpirated);
            return await _tokenService.CreateAccessTokenAsync(user);
        }

        [ValidationAspect(typeof(UserForLoginDtoValidator))]
        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userExistsCheck = await _userOperationService.GetUserByEmailAsync(userForLoginDto.Email);
            if (!userExistsCheck.Success) return new ErrorDataResult<User>(userExistsCheck.Message);
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userExistsCheck.Data.PasswordHash, userExistsCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>(Messages.WrongPassword);
            var operationCheck = BusinessRules.Run(await _userOperationService.IsActiveAsync(userForLoginDto.Email));
            if (operationCheck != null) return new ErrorDataResult<User>(operationCheck.Message);
            return BusinessResultHelper.ReturnData(userExistsCheck.Data, Messages.LoginSuccessful, Messages.NotFound);
        }
    }
}