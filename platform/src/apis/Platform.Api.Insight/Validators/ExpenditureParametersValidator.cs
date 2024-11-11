using FluentValidation;
using Platform.Api.Insight.Expenditure;
namespace Platform.Api.Insight.Validators;

public class ExpenditureParametersValidator : AbstractValidator<ExpenditureParameters>
{
    public ExpenditureParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ExpenditureCategories.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ExpenditureDimensions.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || ExpenditureCategories.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => ExpenditureDimensions.IsValid(dimension);
}