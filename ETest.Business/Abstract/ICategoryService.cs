using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using Core.Utilities.Results.Concrete.Status;
using ETest.Dto.Category;

namespace ETest.Business.Abstract
{
    public interface ICategoryService
    {
        Task<Result> AddAsync(CategoryForAddDto categoryForAddDto);
        Task<Result> UpdateAsync(CategoryForUpdateDto categoryForUpdateDto);
        Task<int> CountAsync();
        IDataResult<List<CategoryForListDto>> GetList();
        Task<IDataResult<CategoryForListDto>> GetBydIdAsync(int id);
    }
}