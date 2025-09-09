using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Domain;

namespace Platform.Api.Insight.Features.ItSpend.Validators;

[ExcludeFromCodeCoverage]
public class ItSpendSchoolsParametersValidator : AbstractValidator<ItSpendSchoolsParameters>
{
    public ItSpendSchoolsParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}