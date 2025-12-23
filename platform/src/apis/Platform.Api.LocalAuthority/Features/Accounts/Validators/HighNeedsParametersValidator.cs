using FluentValidation;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.Accounts.Validators;

public class HighNeedsParametersValidator : AbstractValidator<HighNeedsParameters>
{
    public HighNeedsParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 10)
            .WithMessage("One or more local authority codes must be supplied");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.HighNeeds.All)}");
    }

    private static bool BeAValidDimension(string dimension) => Dimensions.HighNeeds.IsValid(dimension);
}