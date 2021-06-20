using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Utilities.FormFiles;
using Core.Utilities.FormFiles.Abstract;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Business;
using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Business.Abstract;
using ETest.Business.ValidationRules.FluentValidation.User;
using ETest.DataAccess.Abstract;
using ETest.Dto.User;
using ETest.Entities.Concrete;
using ETest.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace ETest.Business.Concrete
{
    public class UserManager:IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IImageCrud _imageCrud;
        private readonly IOperationClaimService _operationClaimService;

        public UserManager(IUserDal userDal, IOperationClaimService operationClaimService, IMapper mapper, IImageCrud imageCrud)
        {
            _userDal = userDal;
            _operationClaimService = operationClaimService;
            _mapper = mapper;
            _imageCrud = imageCrud;
        }

        public async Task<Result> UpdateByUserAsync(User user)
        {
            _userDal.Update(user);
            return BusinessResultHelper.ReturnResult(await _userDal.SaveChangesAsync(), Messages.Updated, Messages.NotUpdated);

        }

        public async Task<Result> ImageUploadAsync(IFormFile image, int userId)
        {
            var userResult = await GetByIdAsync(userId);
            if (!userResult.Success) return new ErrorResult(Messages.NotFound);
            if (!string.IsNullOrEmpty(userResult.Data.ImageUrl))
            {
                var oldImageUrl = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users/" + userResult.Data.ImageUrl);
                FileHelper.Delete(oldImageUrl);
            }

            var imageUrl = ImageUrlHelper.GuidImage();
            await _imageCrud.ImageUploadAsync(image, Path.Combine(ImageUrlHelper.CreateUserImageUrl(),imageUrl), 200, 200);
            userResult.Data.ImageUrl = imageUrl;
            return await UpdateByUserAsync(userResult.Data);
        }

        public async Task<IDataResult<UserOperationClaimModel>> GetUserClaimsByIdAsync(int id)
        {
            var user = await _userDal.GetAsync(x => x.Id == id);
            return BusinessResultHelper.ReturnData(await _userDal.GetClaimsAsync(user), Messages.Found,
                Messages.NotFound);
        }

        public async Task<int> UserCountByStatus(bool status)
        {
            return await _userDal.CountAsync(x => x.IsActive == status);
        }

        [ValidationAspect(typeof(UserForUpdateDtoValidator))]
        public async Task<Result> UpdateAsync(UserForUpdateDto userForUpdateDto)
        {
            var user =await _userDal.GetAsync(x => x.Id == userForUpdateDto.Id);
            if (user == null) return new ErrorResult(Messages.NotFound);
            user = _mapper.Map(userForUpdateDto, user);
            _userDal.Update(user);
            return BusinessResultHelper.ReturnResult(await _userDal.SaveChangesAsync(), Messages.Updated,
                Messages.NotUpdated);

        }

        public async Task<IDataResult<User>> GetByIdAsync(int userId)
        {
            return BusinessResultHelper.ReturnData(await _userDal.GetAsync(x => x.Id == userId), Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<UserForSingleDto>> GetUserSingleDtoByIdAsync(int userId)
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<UserForSingleDto>(await _userDal.GetAsync(x => x.Id == userId)), Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<List<UserForListDto>>> GetListAsync()
        {
            return BusinessResultHelper.ReturnData(_mapper.Map<List<UserForListDto>>(await _userDal.GetListAsync()),
                Messages.Found, Messages.NotFound);
        }

        public async Task<IDataResult<List<UserForOperationClaimDto>>> GetUserOperationClaimDtoByIdAsync(int id)
        {
            var userOperationClaim = await _userDal.GetClaimsAsync(await _userDal.GetAsync(x => x.Id == id));
            var operationClaims = await _operationClaimService.GetOperationClaimsAsync();
            if (operationClaims.Success)
            {
                List<UserForOperationClaimDto> userOperationClaimsDto = new List<UserForOperationClaimDto>();
                foreach (var operationClaim in operationClaims.Data)
                {

                    UserForOperationClaimDto userOperationClaimDto = new UserForOperationClaimDto
                    {
                        IsCheck = userOperationClaim.OperationClaims.Any(x => x.Name == operationClaim.Name),
                        UserName = userOperationClaim.User.UserName,
                        OperationClaimId = operationClaim.Id,
                        UserId = userOperationClaim.User.Id,
                        OperationClaimName = operationClaim.Name
                    };
                    userOperationClaimsDto.Add(userOperationClaimDto);
                }
                return BusinessResultHelper.ReturnData(userOperationClaimsDto, Messages.Found, Messages.NotFound);
            }
            return new ErrorDataResult<List<UserForOperationClaimDto>>(Messages.NotFound);
        }

        public async Task<IDataResult<User>> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var user = await _userDal.GetAsync(x => x.RefreshToken == refreshToken);
            return BusinessResultHelper.ReturnData(user, Messages.Found, Messages.NotFound);
        }
    }
}