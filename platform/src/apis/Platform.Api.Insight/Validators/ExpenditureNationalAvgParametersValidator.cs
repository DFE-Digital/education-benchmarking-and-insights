using FluentValidation;
using Platform.Api.Insight.Domain;
using Platform.Api.Insight.Expenditure;
namespace Platform.Api.Insight.Validators;

public class ExpenditureNationalAvgParametersValidator : AbstractValidator<ExpenditureNationalAvgParameters>
{
    public ExpenditureNationalAvgParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ExpenditureDimensions.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"{{PropertyName}} must one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => ExpenditureDimensions.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}