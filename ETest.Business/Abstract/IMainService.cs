using System.Threading.Tasks;
using Core.Utilities.Results.Abstract.Data;
using ETest.Dto.Main;

namespace ETest.Business.Abstract
{
    public interface IMainService
    {
        Task<IDataResult<MainSingleDto>> GetMainSingleDtoAsync();
    }
}