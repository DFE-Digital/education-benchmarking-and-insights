using FluentValidation;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.MetricRagRatings.Validators;

public class MetricRagRatingSummaryParametersValidator : AbstractValidator<MetricRagRatingSummaryParameters>
{
    public MetricRagRatingSummaryParametersValidator()
    {
        RuleFor(x => x.OverallPhase)
            .Must(BeMissingOrAValidOverallPhase)
            .WithMessage($"{{PropertyName}} must be missing or only contain the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.LaCode) || !string.IsNullOrWhiteSpace(x.CompanyNumber) || x.Urns.Length > 0)
            .WithMessage($"Either {nameof(MetricRagRatingSummaryParameters.LaCode)}, {nameof(MetricRagRatingSummaryParameters.Urns)}, or {nameof(MetricRagRatingSummaryParameters.CompanyNumber)} must be specified.");
    }
    private static bool BeMissingOrAValidOverallPhase(string? overallPhase) => string.IsNullOrWhiteSpace(overallPhase) || OverallPhase.IsValid(overallPhase);
}