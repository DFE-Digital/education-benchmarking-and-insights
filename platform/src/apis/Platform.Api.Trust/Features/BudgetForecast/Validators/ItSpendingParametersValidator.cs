using FluentValidation;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;

namespace Platform.Api.Trust.Features.BudgetForecast.Validators;

public class ItSpendingParametersValidator : AbstractValidator<ItSpendingParameters>
{
    public ItSpendingParametersValidator()
    {
        RuleFor(x => x.CompanyNumbers)
            .NotEmpty();
    }
}