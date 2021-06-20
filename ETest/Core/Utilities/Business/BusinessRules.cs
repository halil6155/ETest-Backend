using Core.Utilities.Results.Abstract.Status;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (var result in results)
            {
                if (!result.Success)
                    return result;
            }

            return null;
        }
    }
}