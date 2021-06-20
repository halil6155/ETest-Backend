using ETest.Dto.OperationClaim;
using FluentValidation;

namespace ETest.Business.ValidationRules.FluentValidation.OperationClaim
{
    public class OperationClaimForUpdateDtoValidator:AbstractValidator<OperationClaimForUpdateDto>
    {
        public OperationClaimForUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).MaximumLength(50).NotEmpty();
        }
    }
}