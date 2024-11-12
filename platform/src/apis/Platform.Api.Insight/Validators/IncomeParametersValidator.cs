using FluentValidation;
using Platform.Api.Insight.Expenditure;
using Platform.Api.Insight.Income;
namespace Platform.Api.Insight.Validators;

public class IncomeParametersValidator : AbstractValidator<IncomeParameters>
{
    public IncomeParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", IncomeCategories.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", IncomeDimensions.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || IncomeCategories.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => ExpenditureDimensions.IsValid(dimension);
}