using ETest.Dto.OperationClaim;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.OperationClaim
{
    public class OperationClaimForAddDtoValidator:AbstractValidator<OperationClaimForAddDto>
    {
        public OperationClaimForAddDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        }
    }
}