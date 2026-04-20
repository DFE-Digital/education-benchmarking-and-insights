using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.Accounts.Validators;

[ExcludeFromCodeCoverage]
public class ExpenditureParametersValidator : AbstractValidator<ExpenditureParameters>
{
    public ExpenditureParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"'{{PropertyName}}' is not a recognized category. Valid values are: {string.Join(", ", Categories.Cost.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"'{{PropertyName}}' is not a recognized dimension. Valid values are: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || Categories.Cost.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}

