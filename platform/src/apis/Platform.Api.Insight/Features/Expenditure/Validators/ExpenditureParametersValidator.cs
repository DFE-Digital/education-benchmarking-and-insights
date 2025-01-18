using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.Expenditure.Validators;

[ExcludeFromCodeCoverage]
public class ExpenditureParametersValidator : AbstractValidator<ExpenditureParameters>
{
    public ExpenditureParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Categories.Cost.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || Categories.Cost.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}