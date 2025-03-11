using FluentValidation;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;

public class HighNeedsDimensionedParametersValidator : AbstractValidator<HighNeedsDimensionedParameters>
{
    public HighNeedsDimensionedParametersValidator()
    {
        Include(new HighNeedsParametersValidator());

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.HighNeeds.All)}");
    }

    private static bool BeAValidDimension(string? dimension) => string.IsNullOrWhiteSpace(dimension) || Dimensions.HighNeeds.IsValid(dimension);
}