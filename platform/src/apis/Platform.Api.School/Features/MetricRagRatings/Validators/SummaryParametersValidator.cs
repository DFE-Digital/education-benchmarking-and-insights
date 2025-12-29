using FluentValidation;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.MetricRagRatings.Validators;

public class SummaryParametersValidator : AbstractValidator<SummaryParameters>
{
    public SummaryParametersValidator()
    {
        RuleFor(x => x.OverallPhase)
            .Must(BeMissingOrAValidOverallPhase)
            .WithMessage($"{{PropertyName}} must be missing or only contain the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.LaCode) || !string.IsNullOrWhiteSpace(x.CompanyNumber) || x.Urns.Length > 0)
            .WithMessage($"Either {nameof(SummaryParameters.LaCode)}, {nameof(SummaryParameters.Urns)}, or {nameof(SummaryParameters.CompanyNumber)} must be specified.");
    }
    private static bool BeMissingOrAValidOverallPhase(string? overallPhase) => string.IsNullOrWhiteSpace(overallPhase) || OverallPhase.IsValid(overallPhase);
}