using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.User;
using ETest.Entities.Concrete;
using ETest.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace ETest.Business.Abstract
{
    public interface IUserService
    {
        Task<Result> UpdateByUserAsync(User user);
        Task<Result> ImageUploadAsync(IFormFile image, int userId);
        Task<IDataResult<UserOperationClaimModel>> GetUserClaimsByIdAsync(int id);
        Task<int> UserCountByStatus(bool status);
        Task<Result> UpdateAsync(UserForUpdateDto userForUpdateDto);
        Task<IDataResult<User>> GetByIdAsync(int userId);
        Task<IDataResult<UserForSingleDto>> GetUserSingleDtoByIdAsync(int userId);
        Task<IDataResult<List<UserForListDto>>> GetListAsync();
        Task<IDataResult<List<UserForOperationClaimDto>>> GetUserOperationClaimDtoByIdAsync(int id);
        Task<IDataResult<User>> GetUserByRefreshTokenAsync(string refreshToken);
    }
}