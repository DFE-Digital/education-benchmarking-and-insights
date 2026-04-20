using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.Accounts.Validators;

[ExcludeFromCodeCoverage]
public class BalanceParametersValidator : AbstractValidator<BalanceParameters>
{
    public BalanceParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"'{{PropertyName}}' is not a recognized dimension. Valid values are: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}

