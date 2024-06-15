using FluentValidation;
using Web.App.Domain;

namespace Web.App.Validators.FinancialPlanStages;

public class PrePopulateDataStageValidator : AbstractValidator<PrePopulateDataStage>
{
    public PrePopulateDataStageValidator()
    {
        RuleFor(p => p.UseFigures)
            .Must(x => x.HasValue)
            .WithMessage("Select yes if you want to use these figures");
    }
}