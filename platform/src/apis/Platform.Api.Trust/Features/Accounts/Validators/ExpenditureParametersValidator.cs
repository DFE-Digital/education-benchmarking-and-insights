using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.Trust.Features.Accounts.Validators;

[ExcludeFromCodeCoverage]
public class ExpenditureParametersValidator : AbstractValidator<ExpenditureParameters>
{
    public ExpenditureParametersValidator()
    {
        RuleFor(x => x.Category)
            .Must(BeAnEmptyOrValidCategory)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Categories.Cost.All)}");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.Finance.All)}");
    }

    private static bool BeAnEmptyOrValidCategory(string? category) => string.IsNullOrWhiteSpace(category) || Categories.Cost.IsValid(category);
    private static bool BeAValidDimension(string? dimension) => Dimensions.Finance.IsValid(dimension);
}

[ExcludeFromCodeCoverage]
public class ExpenditureQueryParametersValidator : AbstractValidator<ExpenditureQueryParameters>
{
    public ExpenditureQueryParametersValidator()
    {
        Include(new ExpenditureParametersValidator());

        RuleFor(x => x.CompanyNumbers)
            .NotEmpty()
            .WithMessage("A collection of company numbers must be specified");
    }
}