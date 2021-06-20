using System.Linq;
using FluentValidation;

namespace Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                var errors = result.Errors.Aggregate("", (current, error) => $"{current}{error.ErrorMessage}\n");
                throw new ValidationException(errors);
            }
        }
    }
}