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
            .WithMessage($"'{{PropertyName}}' is not a recognized census dimension. Valid values are: {string.Join(", ", Dimensions.Census.All)}");

        RuleFor(x => x.OverallPhase)
            .Must(BeAValidPhase)
            .WithMessage($"'{{PropertyName}}' is not a recognized school phase. Valid values are: {string.Join(", ", OverallPhase.All)}");

        RuleFor(x => x.FinanceType)
            .Must(BeAValidFinanceType)
            .WithMessage($"'{{PropertyName}}' is not a recognized finance type. Valid values are: {string.Join(", ", FinanceType.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Census.IsValid(dimension);
    private static bool BeAValidPhase(string? phase) => OverallPhase.IsValid(phase);
    private static bool BeAValidFinanceType(string? financeType) => FinanceType.IsValid(financeType);
}