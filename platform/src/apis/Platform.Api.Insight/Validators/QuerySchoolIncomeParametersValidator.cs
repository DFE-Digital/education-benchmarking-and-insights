using FluentValidation;
using Platform.Api.Insight.Domain;
using Platform.Api.Insight.Income;
namespace Platform.Api.Insight.Validators;

public class QuerySchoolIncomeParametersValidator : AbstractValidator<QuerySchoolIncomeParameters>
{
    public QuerySchoolIncomeParametersValidator()
    {
        Include(new IncomeParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(QuerySchoolIncomeParameters.CompanyNumber)} or {nameof(QuerySchoolIncomeParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(QuerySchoolIncomeParameters.CompanyNumber)} or {nameof(QuerySchoolIncomeParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(QuerySchoolIncomeParameters.CompanyNumber)} or {nameof(QuerySchoolIncomeParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}