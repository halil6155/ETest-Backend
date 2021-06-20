using Core.Utilities.Results.Abstract.Status;

namespace Core.Utilities.Results.Abstract.Data
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}