using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.ItSpend.Parameters;

namespace Platform.Api.Insight.Features.ItSpend.Validators;

[ExcludeFromCodeCoverage]
public class ItSpendTrustForecastParametersValidator : AbstractValidator<ItSpendTrustForecastParameters>
{
    public ItSpendTrustForecastParametersValidator()
    {
        RuleFor(x => x.CompanyNumber)
            .NotEmpty();
        RuleFor(x => x.Year)
            .NotEmpty();
    }
}