﻿using System.Linq;
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
            .When(x => string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"Either a collection of URNs or one of {nameof(MetricRagRatingsParameters.CompanyNumber)} or {nameof(MetricRagRatingsParameters.LaCode)} must be specified");

        RuleFor(x => x.LaCode)
            .Empty()
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.CompanyNumber))
            .WithMessage($"Either {nameof(MetricRagRatingsParameters.CompanyNumber)} or {nameof(MetricRagRatingsParameters.LaCode)} must be specified, not both");

        RuleFor(x => x.Phase)
            .Must(BeAValidPhase)
            .When(x => x.Urns.Length == 0)
            .When(x => !string.IsNullOrWhiteSpace(x.LaCode))
            .WithMessage($"{{PropertyName}} must be be specified when {nameof(MetricRagRatingsParameters.LaCode)} is supplied and be one of the supported values: {string.Join(", ", OverallPhase.All)}");
    }

    private static bool ContainValidCategories(string[] categories) => categories.All(CostCategories.IsValid);
    private static bool ContainValidStatuses(string[] statuses) => statuses.All(RagRating.IsValid);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
}