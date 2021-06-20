using FluentValidation;
 
namespace ETest.Business.ValidationRules.FluentValidation.User
{
    public class UserForRegisterDtoValidator:AbstractValidator<Entities.Concrete.User>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(25).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(25).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(25).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(50).EmailAddress();
        }
    }
}