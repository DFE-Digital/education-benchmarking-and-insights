using EducationBenchmarking.Platform.Shared.Characteristics;
using FluentValidation;

namespace EducationBenchmarking.Platform.Shared.Validators;

public class SchoolComparatorSetRequestValidator : AbstractValidator<SchoolComparatorSetRequest>
{
    public SchoolComparatorSetRequestValidator()
    {
        RuleFor(p => p.Characteristics)
            .Must(x => x is null || x.Keys.All(key => Questions.Schools.AllCodes.Contains(key)))
            .WithMessage("Invalid characteristics");
    }
}