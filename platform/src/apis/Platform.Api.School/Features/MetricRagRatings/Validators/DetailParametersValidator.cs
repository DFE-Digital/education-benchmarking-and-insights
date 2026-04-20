using System.Linq;
using FluentValidation;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.MetricRagRatings.Validators;

public class DetailParametersValidator : AbstractValidator<DetailParameters>
{
    public DetailParametersValidator()
    {
        RuleFor(x => x.Categories)
            .Must(ContainValidCategories)
            .WithMessage($"'{{PropertyName}}' contains unsupported values. Valid cost categories are: {string.Join(", ", CostCategories.All)}");

        RuleFor(x => x.Statuses)
            .Must(ContainValidStatuses)
            .WithMessage($"'{{PropertyName}}' contains unsupported values. Valid RAG status values are: {string.Join(", ", RagRating.All)}");

        RuleFor(x => x.Urns)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either a collection of school URNs or a trust {nameof(DetailParameters.CompanyNumber)} must be provided to query metric RAG ratings.");
    }

    private static bool ContainValidCategories(string[] categories) => categories.All(CostCategories.IsValid);
    private static bool ContainValidStatuses(string[] statuses) => statuses.All(RagRating.IsValid);
}