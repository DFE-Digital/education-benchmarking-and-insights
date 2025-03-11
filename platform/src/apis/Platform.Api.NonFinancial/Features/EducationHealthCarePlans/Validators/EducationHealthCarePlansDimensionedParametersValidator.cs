using FluentValidation;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Domain;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Validators;

public class EducationHealthCarePlansDimensionedParametersValidator : AbstractValidator<EducationHealthCarePlansDimensionedParameters>
{
    public EducationHealthCarePlansDimensionedParametersValidator()
    {
        Include(new EducationHealthCarePlansParametersValidator());

        RuleFor(x => x.Dimension)
            .Must(BeAValidDimension)
            .WithMessage($"{{PropertyName}} must be empty or one of the supported values: {string.Join(", ", Dimensions.EducationHealthCarePlans.All)}");
    }

    private static bool BeAValidDimension(string dimension) => Dimensions.EducationHealthCarePlans.IsValid(dimension);
}