namespace Core.Utilities.Results.Api
{
    public class ApiDataResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
}