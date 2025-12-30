using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.Census.Validators;

[ExcludeFromCodeCoverage]
public class NationalAvgParametersValidator : AbstractValidator<NationalAvgParameters>
{
    public NationalAvgParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Census.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"{{PropertyName}} must be one of the supported values: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Census.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}