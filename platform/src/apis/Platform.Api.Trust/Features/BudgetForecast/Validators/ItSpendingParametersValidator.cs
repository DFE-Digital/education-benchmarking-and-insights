using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;

namespace Platform.Api.Trust.Features.BudgetForecast.Validators;

[ExcludeFromCodeCoverage]
public class ItSpendingParametersValidator : AbstractValidator<ItSpendingParameters>
{
    public ItSpendingParametersValidator()
    {
        RuleFor(x => x.CompanyNumbers)
            .NotEmpty();
    }
}