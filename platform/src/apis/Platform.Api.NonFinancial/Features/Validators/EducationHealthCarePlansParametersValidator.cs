using System.Linq;
using FluentValidation;
using Platform.Domain;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;

namespace Platform.Api.NonFinancial.Features.Validators;

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
    }
}