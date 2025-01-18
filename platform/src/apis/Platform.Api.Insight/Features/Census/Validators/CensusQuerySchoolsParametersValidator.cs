using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Census.Validators;

[ExcludeFromCodeCoverage]
public class CensusQuerySchoolsParametersValidator : AbstractValidator<CensusQuerySchoolsParameters>
{
    public CensusQuerySchoolsParametersValidator()
    {
        Include(new CensusParametersValidator());

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber) || !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(CensusQuerySchoolsParameters.CompanyNumber)} or {nameof(CensusQuerySchoolsParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}