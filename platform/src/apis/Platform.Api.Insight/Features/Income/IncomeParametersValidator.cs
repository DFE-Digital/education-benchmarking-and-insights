using FluentValidation;

namespace Platform.Api.Insight.Income;

public class IncomeParametersValidator : AbstractValidator<IncomeParameters>
{
    public IncomeParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", IncomeDimensions.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => IncomeDimensions.IsValid(dimension);
}