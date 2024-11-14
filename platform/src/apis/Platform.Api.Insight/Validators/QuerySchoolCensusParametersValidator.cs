using FluentValidation;
using Platform.Api.Insight.Census;
using Platform.Api.Insight.Domain;
namespace Platform.Api.Insight.Validators;

public class QuerySchoolCensusParametersValidator : AbstractValidator<QuerySchoolCensusParameters>
{
    public QuerySchoolCensusParametersValidator()
    {
        Include(new CensusParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(QuerySchoolCensusParameters.CompanyNumber)} or {nameof(QuerySchoolCensusParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}