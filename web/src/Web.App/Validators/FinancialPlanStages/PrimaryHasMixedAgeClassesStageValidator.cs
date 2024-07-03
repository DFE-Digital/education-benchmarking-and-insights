using FluentValidation;
using Web.App.Domain;
namespace Web.App.Validators.FinancialPlanStages;

public class PrimaryHasMixedAgeClassesStageValidator : AbstractValidator<PrimaryHasMixedAgeClassesStage>
{
    public PrimaryHasMixedAgeClassesStageValidator()
    {
        RuleFor(p => p.HasMixedAgeClasses)
            .NotNull()
            .WithMessage("Select if you have any mixed age classes");
    }
}