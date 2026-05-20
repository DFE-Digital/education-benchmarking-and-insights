using System.Linq;
using FluentValidation;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.Accounts.Validators;

public class HighNeedsParametersValidatorV2 : AbstractValidator<HighNeedsParametersV2>
{
    public HighNeedsParametersValidatorV2()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .Must(c => c.Length <= 30)
            .WithMessage("Between 1 and 30 local authority codes must be supplied");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidDimensions)}");

        RuleFor(x => x.Type)
            .Must(BeAValidType)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", ValidSubmissionTypes)}");
    }

    internal static readonly string[] ValidDimensions =
    [
        Dimensions.HighNeeds.PerPupil,
        Dimensions.HighNeeds.PerEhcp,
        Dimensions.HighNeeds.PerSenSupport,
        Dimensions.HighNeeds.PerTotalSupport
    ];

    internal static readonly string[] ValidSubmissionTypes =
    [
        SubmissionType.Budget,
        SubmissionType.Outturn,
    ];

    private static bool BeAValidDimension(string dimension) => ValidDimensions.Any(d => d == dimension);
    private static bool BeAValidType(string type) => ValidSubmissionTypes.Any(t => t == type);
}

// expose the validator values for use in the swagger docs
public static class HighNeedsValidatorV2Values
{
    public static string[] Dimensions => HighNeedsParametersValidatorV2.ValidDimensions;
    public static string[] Types => HighNeedsParametersValidatorV2.ValidSubmissionTypes;
}
