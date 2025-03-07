using FluentValidation;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;

public class HighNeedsParametersValidator : AbstractValidator<HighNeedsParameters>
{
    public HighNeedsParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 10)
            .WithMessage("One or more local authority codes must be supplied");
    }
}