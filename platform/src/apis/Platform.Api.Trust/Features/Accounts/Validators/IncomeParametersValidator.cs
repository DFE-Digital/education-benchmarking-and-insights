using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.Trust.Features.Accounts.Validators;

[ExcludeFromCodeCoverage]
public class IncomeParametersValidator : AbstractValidator<IncomeParameters>
{
    public IncomeParametersValidator()
    {
        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}