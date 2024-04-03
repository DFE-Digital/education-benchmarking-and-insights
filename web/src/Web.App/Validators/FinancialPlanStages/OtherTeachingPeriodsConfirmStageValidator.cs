using FluentValidation;
using Web.App.Domain.FinancialPlanStages;

namespace Web.App.Validators.FinancialPlanStages;

public class OtherTeachingPeriodsConfirmStageValidator : AbstractValidator<OtherTeachingPeriodsConfirmStage>
{
    public OtherTeachingPeriodsConfirmStageValidator()
    {
        RuleFor(p => p.Proceed)
            .Must(x => x.HasValue)
            .WithMessage("Select yes if you want to continue without adding other teaching periods");
    }
}