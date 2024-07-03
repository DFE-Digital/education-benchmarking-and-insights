using FluentValidation;
using Web.App.Domain;
namespace Web.App.Validators.FinancialPlanStages;

public class OtherTeachingPeriodsConfirmStageValidator : AbstractValidator<OtherTeachingPeriodsConfirmStage>
{
    public OtherTeachingPeriodsConfirmStageValidator()
    {
        RuleFor(p => p.Proceed)
            .Must(x => x.HasValue)
            .WithMessage("Confirm if you want to proceed without adding other teaching periods");
    }
}