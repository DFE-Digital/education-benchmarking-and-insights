using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Domain;

namespace Platform.Api.School.Features.Census.Validators;

[ExcludeFromCodeCoverage]
public class SeniorLeadershipParametersValidator : AbstractValidator<SeniorLeadershipParameters>
{
    public SeniorLeadershipParametersValidator()
    {
        RuleFor(x => x.Urns)
            .NotEmpty()
            .WithMessage("'{PropertyName}' must be specified");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"'{{PropertyName}}' is not a recognized census dimension. Valid values are: {Dimensions.Census.Total} or {Dimensions.Census.PercentWorkforce}");
    }

    private static bool BeAValidDimension(string? dimension) => dimension is Dimensions.Census.Total or Dimensions.Census.PercentWorkforce;
}

