using FluentValidation;
using Platform.Api.Insight.Census;
using Platform.Api.Insight.Domain;

namespace Platform.Api.Insight.Validators;

public class CensusNationalAvgParametersValidator : AbstractValidator<CensusNationalAvgParameters>
{
    public CensusNationalAvgParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", CensusDimensions.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => CensusDimensions.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}