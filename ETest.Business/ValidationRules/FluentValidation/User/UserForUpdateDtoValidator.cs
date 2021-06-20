using ETest.Dto.User;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.User
{
    public class UserForUpdateDtoValidator:AbstractValidator<UserForUpdateDto>
    {
        public UserForUpdateDtoValidator()
        {
 
            RuleFor(x => x.FirstName).MaximumLength(25).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(25).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(50).EmailAddress();
        }
    }
}