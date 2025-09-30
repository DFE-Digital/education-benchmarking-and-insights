using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.ItSpend.Parameters;

namespace Platform.Api.Insight.Features.ItSpend.Validators;

[ExcludeFromCodeCoverage]
public class ItSpendTrustsParametersValidator : AbstractValidator<ItSpendTrustsParameters>
{
    public ItSpendTrustsParametersValidator()
    {
        RuleFor(x => x.CompanyNumbers)
            .NotEmpty();
    }
}