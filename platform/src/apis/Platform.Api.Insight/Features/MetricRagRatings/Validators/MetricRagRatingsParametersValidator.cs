using System.Linq;
using FluentValidation;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.MetricRagRatings.Validators;

public class MetricRagRatingsParametersValidator : AbstractValidator<MetricRagRatingsParameters>
{
    public MetricRagRatingsParametersValidator()
    {
        RuleFor(x => x.Categories)
            .Must(ContainValidCategories)
            .WithMessage($"{{PropertyName}} must only contain the supported values: {string.Join(", ", CostCategories.All)}");

        RuleFor(x => x.Statuses)
            .Must(ContainValidStatuses)
            .WithMessage($"{{PropertyName}} must only contain the supported values: {string.Join(", ", RagRating.All)}");

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either a collection of URNs or {nameof(MetricRagRatingsParameters.CompanyNumber)} must be specified");
    }

    private static bool ContainValidCategories(string[] categories) => categories.All(CostCategories.IsValid);
    private static bool ContainValidStatuses(string[] statuses) => statuses.All(RagRating.IsValid);
}