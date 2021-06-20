namespace Core.Utilities.Results.Abstract.Status
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}