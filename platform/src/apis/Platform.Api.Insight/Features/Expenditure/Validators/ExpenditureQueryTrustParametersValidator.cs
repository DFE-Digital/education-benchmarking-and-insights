using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.Expenditure.Parameters;

namespace Platform.Api.Insight.Features.Expenditure.Validators;

[ExcludeFromCodeCoverage]
public class ExpenditureQueryTrustParametersValidator : AbstractValidator<ExpenditureQueryTrustParameters>
{
    public ExpenditureQueryTrustParametersValidator()
    {
        Include(new ExpenditureParametersValidator());

        RuleFor(x => x.CompanyNumbers)
            .NotEmpty()
            .WithMessage("A collection of company numbers must be specified");
    }
}