using FluentValidation;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;

public class HighNeedsHistoryParametersValidator : AbstractValidator<HighNeedsHistoryParameters>
{
    public HighNeedsHistoryParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 10)
            .WithMessage("One or more local authority codes must be supplied");
    }
}