using FluentValidation;
using Web.App.Domain.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages;

public class PrimaryHasMixedAgeClassesStageValidator : AbstractValidator<PrimaryHasMixedAgeClassesStage>
{
    public PrimaryHasMixedAgeClassesStageValidator()
    {
        RuleFor(p => p.HasMixedAgeClasses)
            .NotNull()
            .WithMessage("Select yes if you have mixed age classes");
    }
}