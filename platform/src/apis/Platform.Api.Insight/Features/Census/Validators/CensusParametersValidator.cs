using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Census.Validators;

[ExcludeFromCodeCoverage]
public class CensusParametersValidator : AbstractValidator<CensusParameters>
{
    public CensusParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Categories.Census.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Census.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || Categories.Census.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => Dimensions.Census.IsValid(dimension);
}