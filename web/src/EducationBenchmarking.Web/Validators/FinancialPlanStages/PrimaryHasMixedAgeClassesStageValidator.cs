using EducationBenchmarking.Web.Domain.FinancialPlanStages;
using FluentValidation;

namespace EducationBenchmarking.Web.Validators.FinancialPlanStages;

public class PrimaryHasMixedAgeClassesStageValidator : AbstractValidator<PrimaryHasMixedAgeClassesStage>
{
    public PrimaryHasMixedAgeClassesStageValidator()
    {
        RuleFor(p => p.HasMixedAgeClasses)
            .NotNull()
            .WithMessage("Select yes if you have mixed age classes");
    }
}