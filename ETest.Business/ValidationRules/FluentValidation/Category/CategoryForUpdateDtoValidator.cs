using ETest.Dto.Category;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.Category
{
    public class CategoryForUpdateDtoValidator:AbstractValidator<CategoryForUpdateDto>
    {
        public CategoryForUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.Description).MaximumLength(100);
        }
    }
}