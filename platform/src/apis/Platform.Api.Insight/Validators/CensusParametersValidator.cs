using FluentValidation;
using Platform.Api.Insight.Census;

namespace Platform.Api.Insight.Validators;

public class CensusParametersValidator : AbstractValidator<CensusParameters>
{
    public CensusParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusCategories.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusDimensions.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || CensusCategories.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => CensusDimensions.IsValid(dimension);
}