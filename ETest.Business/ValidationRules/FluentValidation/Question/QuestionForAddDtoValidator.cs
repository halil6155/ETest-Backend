using ETest.Dto.Question;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.Question
{
    public class QuestionForAddDtoValidator:AbstractValidator<QuestionForAddDto>
    {
        public QuestionForAddDtoValidator()
        {
            RuleFor(x => x.Text).MaximumLength(500).NotEmpty();
            RuleFor(x => x.OptionA).MaximumLength(100).NotEmpty();
            RuleFor(x => x.OptionB).MaximumLength(100).NotEmpty();
            RuleFor(x => x.OptionC).MaximumLength(100).NotEmpty();
            RuleFor(x => x.OptionD).MaximumLength(100).NotEmpty();
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.Answer).NotNull();
        }
    }
}