using FluentValidation;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Domain;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Validators;

public class EducationHealthCarePlansParametersValidator : AbstractValidator<EducationHealthCarePlansParameters>
{
    public EducationHealthCarePlansParametersValidator()
    {
        RuleFor(x => x.Codes)
            .NotEmpty()
            .WithMessage("Between 1 and 10 codes must be supplied");

        RuleFor(x => x.Codes)
            .Must(codes => codes.Length <= 10)
            .WithMessage("Between 1 and 10 codes must be supplied");

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.EducationHealthCarePlans.All)}");
    }

    private static bool BeAValidDimension(string dimension) => Dimensions.EducationHealthCarePlans.IsValid(dimension);
}