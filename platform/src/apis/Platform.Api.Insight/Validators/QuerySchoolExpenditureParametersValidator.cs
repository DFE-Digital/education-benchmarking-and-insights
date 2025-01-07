using FluentValidation;
using Platform.Domain;
using Platform.Api.Insight.Expenditure;
namespace Platform.Api.Insight.Validators;

public class QuerySchoolExpenditureParametersValidator : AbstractValidator<QuerySchoolExpenditureParameters>
{
    public QuerySchoolExpenditureParametersValidator()
    {
        Include(new ExpenditureParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(QuerySchoolExpenditureParameters.CompanyNumber)} or {nameof(QuerySchoolExpenditureParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(QuerySchoolExpenditureParameters.CompanyNumber)} or {nameof(QuerySchoolExpenditureParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(QuerySchoolExpenditureParameters.CompanyNumber)} or {nameof(QuerySchoolExpenditureParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}