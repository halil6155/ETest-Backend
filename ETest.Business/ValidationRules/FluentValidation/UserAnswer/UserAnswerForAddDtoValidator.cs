using ETest.Dto.UserAnswer;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.UserAnswer
{
    public class UserAnswerForAddDtoValidator:AbstractValidator<UserAnswerForAddDto>
    {
        public UserAnswerForAddDtoValidator()
        {
            RuleFor(x => x.Answer).NotNull();
            RuleFor(x => x.QuestionId).NotNull().GreaterThan(0);
            RuleFor(x => x.UserId).NotNull().GreaterThan(0);
        }
    }
}