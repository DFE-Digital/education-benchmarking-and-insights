using FluentValidation;
using Platform.Api.Insight.Expenditure;
namespace Platform.Api.Insight.Validators;

public class QueryTrustExpenditureParametersValidator : AbstractValidator<QueryTrustExpenditureParameters>
{
    public QueryTrustExpenditureParametersValidator()
    {
        Include(new ExpenditureParametersValidator());

        RuleFor(x => x.CompanyNumbers)
            .NotEmpty()
            .WithMessage("A collection of company numbers must be specified");
    }
}