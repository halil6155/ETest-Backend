using Core.Utilities.Results.Concrete.Data;
using Core.Utilities.Results.Concrete.Status;

namespace Core.Utilities.Results.Business
{
    public class BusinessResultHelper
    {
        public static DataResult<T> ReturnData<T>(T data, string successMessage, string errorMessage)
        {
            if (data != null)
                return new SuccessDataResult<T>(data, successMessage);
            return new ErrorDataResult<T>(errorMessage);
        }


   
            public static Result ReturnResult<T>(T data, string successMessage, string errorMessage)
        {
            if (data != null)
                return new SuccessResult(successMessage);
            return new ErrorResult(errorMessage);
        }
        public static Result ReturnResult(int result, string successMessage, string errorMessage)
        {
            if (result>0)
                return new SuccessResult(successMessage);
            return new ErrorResult(errorMessage);
        }
    }
}