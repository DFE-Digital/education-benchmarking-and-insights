using FluentValidation;
using Web.App.Domain;
namespace Web.App.Validators.FinancialPlanStages;

public class SelectYearStageValidator : AbstractValidator<SelectYearStage>
{
    public SelectYearStageValidator()
    {
        RuleFor(p => p.Year)
            .Must(x => x.HasValue && Constants.AvailableYears.Contains(x.Value))
            .WithMessage("Select an academic year to plan");
    }
}