using System.Linq;
using FluentValidation;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.Accounts.Validators;

public class HighNeedsParametersValidatorV1 : AbstractValidator<HighNeedsParametersV1>
{
    public HighNeedsParametersValidatorV1()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 30)
            .WithMessage("Between 1 and 30 local authority codes must be supplied");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidDimensions)}");
    }

    private static readonly string[] ValidDimensions =
    [
        Dimensions.HighNeeds.Actuals,
        Dimensions.HighNeeds.PerHead,
        Dimensions.HighNeeds.PerPupil
    ];

    private static bool BeAValidDimension(string dimension) => ValidDimensions.Any(d => d == dimension);
}