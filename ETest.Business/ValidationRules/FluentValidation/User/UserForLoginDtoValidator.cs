using ETest.Dto.User;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.User
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(x => x.Email).MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(5).MaximumLength(50);
        }
    }
}