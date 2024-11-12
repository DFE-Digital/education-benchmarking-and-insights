using FluentValidation;
using Platform.Api.Insight.Income;
namespace Platform.Api.Insight.Validators;

public class QueryTrustIncomeParametersValidator : AbstractValidator<QueryTrustIncomeParameters>
{
    public QueryTrustIncomeParametersValidator()
    {
        Include(new IncomeParametersValidator());

        RuleFor(x => x.CompanyNumbers)
            .NotEmpty()
            .WithMessage("A collection of company numbers must be specified");
    }
}